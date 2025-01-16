import useSessionStore from "@/stores/useSessionStore";
import useToastsStore from "@/stores/useToastsStore";
import { Session } from "next-auth";

const baseUrl: string | undefined = process.env.NEXT_PUBLIC_API_HOST;

async function request(
  method: string,
  endpoint: string,
  body?: any | undefined
): Promise<any> {
  try {
    const session: Session | null = useSessionStore.getState().session;

    const options: any = {
      method,
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${session?.accessToken}`,
      },
    };

    if (body) {
      options.body = JSON.stringify(body);
    }

    const response = await fetch(`${baseUrl}${endpoint}`, options);

    if (!response.ok) {
      const body = response.bodyUsed ? await response.json() : undefined;
      throw new Error(
        `HTTP error, status: ${response.status}, message: ${body?.title ?? ""}`
      );
    }

    const contentType = response.headers.get("content-type");
    if (contentType && contentType.indexOf("application/json") !== -1) {
      const data = await response.json();
      return data;
    }

    return response.text();
  } catch (error: any) {
    const message = error instanceof Error ? error.message : `Unknown error`;
    console.error(`There was an error with your request: ${message}`);
    useToastsStore.getState().addToast({
      title: "Error",
      description: `There was an error with your request: ${message}`,
      variant: "destructive",
    });
    throw error;
  }
}

const get = (endpoint: string): Promise<any> => request("GET", endpoint);
const post = (endpoint: string, body: Record<string, any>): Promise<any> =>
  request("POST", endpoint, body);
const put = (endpoint: string, body: Record<string, any>): Promise<any> =>
  request("PUT", endpoint, body);
const deleteRequest = (endpoint: string): Promise<any> =>
  request("DELETE", endpoint);

export { deleteRequest, get, post, put };
