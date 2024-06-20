class ApiClient {
  private baseUrl: string | undefined = process.env.NEXT_PUBLIC_API_HOST;

  private async request(
    method: string,
    endpoint: string,
    body?: any | undefined
  ): Promise<any> {
    try {
      const options: any = {
        method,
        headers: {
          "Content-Type": "application/json",
        },
      };

      if (body) {
        options.body = JSON.stringify(body);
      }

      const response = await fetch(`${this.baseUrl}${endpoint}`, options);

      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
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
      throw error;
    }
  }

  get(endpoint: string) {
    return this.request("GET", endpoint);
  }

  post(endpoint: string, body: any) {
    return this.request("POST", endpoint, body);
  }

  put(endpoint: string, body: any) {
    return this.request("PUT", endpoint, body);
  }

  delete(endpoint: string) {
    return this.request("DELETE", endpoint);
  }
}

export default ApiClient;
