import { signIn } from "@/auth";
import {
  Button,
  Card,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/index";

export default async function LoginPage() {
  return (
    <div className="min-h-screen flex justify-center items-start md:items-center p-8">
      <Card className="w-full max-w-sm">
        <CardHeader>
          <CardTitle className="text-2xl">Login</CardTitle>
          <CardDescription>
            DigitalNotes requires you to be authenticated in order to use this
            service. You can use Google for authentication or sign up using your
            email address.
          </CardDescription>
        </CardHeader>
        <CardFooter>
          <form
            action={async () => {
              "use server";
              await signIn("github", {
                redirectTo: "/",
              });
            }}
            className="w-full"
          >
            <Button className="w-full">Proceed to Login</Button>
          </form>
        </CardFooter>
      </Card>
    </div>
  );
}
