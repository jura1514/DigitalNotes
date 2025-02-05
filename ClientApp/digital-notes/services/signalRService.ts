import {
    HttpTransportType,
    HubConnection,
    HubConnectionBuilder,
    LogLevel,
} from "@microsoft/signalr";

let connections = {} as {
  [key: string]: { type: string; connection: HubConnection; started: boolean };
};

function createConnection(
  accessToken: string,
  endpoint: string
): HubConnection {
  const existingConnection = connections[endpoint];
  if (!existingConnection) {
    const connection = new HubConnectionBuilder()
      .withUrl(`http://localhost:5089/hubs/${endpoint}`, {
        skipNegotiation: true,
        transport: HttpTransportType.WebSockets,
        accessTokenFactory: () => accessToken,
      })
      .withAutomaticReconnect()
      .configureLogging(LogLevel.Debug)
      .build();

    connections[endpoint] = {
      type: endpoint,
      connection,
      started: false,
    };

    return connection;
  } else {
    return connections[endpoint].connection;
  }
}

function startConnection(endpoint: string) {
  const existingConnection = connections[endpoint];
  if (!existingConnection.started) {
    existingConnection.connection
      .start()
      .then(() => {
        console.log("Connection started");
        existingConnection.started = true;
      })
      .catch((error) => console.error("SOCKET: ", error.toString()));
  }
}

function stopConnection(endpoint: string) {
  const existingConnection = connections[endpoint];
  if (existingConnection && existingConnection.started) {
    console.log("SOCKET: Stopping connection ", endpoint);
    existingConnection.connection.stop().then(() => {
      existingConnection.started = false;
    });
  }
}

export const signalRService = {
  createConnection,
  startConnection,
  stopConnection,
};
