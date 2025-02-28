import fastify from "fastify";
import { authRoutes } from "./routes/appRoutes.ts";
import "dotenv";
import jwt from "@fastify/jwt";
import { config } from "dotenv";

config();

const app = fastify({ logger: true });

const appListenOptions = {
  port: 3001,
};

app.register(jwt, {
  secret: process.env.JWT_SECRET, // Вынеси в .env
});

app.register(authRoutes);

app.listen(appListenOptions, async () => {
  console.log("Server listening on http://localhost:3001");
});
