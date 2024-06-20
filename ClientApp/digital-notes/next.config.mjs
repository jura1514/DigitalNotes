/** @type {import('next').NextConfig} */
const nextConfig = {
    reactStrictMode: true,
};
process.env["NODE_TLS_REJECT_UNAUTHORIZED"] = 0;
export default nextConfig;
