/** @type {import('next').NextConfig} */
const nextConfig = {
    reactStrictMode: false,
};
process.env["NODE_TLS_REJECT_UNAUTHORIZED"] = 0;
export default nextConfig;
