import NextAuth from "next-auth";
import Auth0 from "next-auth/providers/auth0";

declare module "next-auth" {
  /**
   * Returned by `auth`, `useSession`, `getSession` and received as a prop on the `SessionProvider` React Context
   */
  interface Session {
    accessToken: string;
  }
}

export const { handlers, signIn, signOut, auth } = NextAuth({
  providers: [
    Auth0({
      authorization: {
        params: {
          audience: process.env.AUTH_AUTH0_AUDIENCE,
        },
        scope: "openid email profile"
      },
    }),
  ],
  callbacks: {
    async jwt({ token, trigger, session, account }) {
      if (account?.provider === "auth0") {
        return { ...token, accessToken: account?.access_token };
      }
      return token;
    },
    async session({ session, token }) {
      session.accessToken = token.accessToken as string;
      return session;
    },
  },
});
