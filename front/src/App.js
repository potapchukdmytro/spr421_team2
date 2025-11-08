import { HubConnectionBuilder } from "@microsoft/signalr";
import WaitingRoom from "./components/WaitingRoom";
import { Chat } from "./components/Chat";
import { useState } from "react";

function App() {
  const [connection, setConnection] = useState(null);
  const [messages, setMessages] = useState([]);
  const [chatRoom, setChatRoom] = useState("");
  const [userName, setUserName] = useState("");

  const joinChat = async (userName, chatRoom) => {
    const connection = new HubConnectionBuilder()
      .withUrl("http://localhost:5120/tg")
      .withAutomaticReconnect()
      .build();

    connection.on("ReceiveMessage", (userName, message) => {
      setMessages((prevMessages) => [...prevMessages, { userName, message }]);
    });

    try {
      await connection.start();
      await connection.invoke("JoinChat", { userName, chatRoom });

      setUserName(userName); 
      setConnection(connection);
      setChatRoom(chatRoom);
    } catch (error) {
      console.log("Помилка підключення:", error);
    }
  };

  const sendMessage = async (message) => {
    if (message && connection) {
      try {
        await connection.invoke("SendMessage", message);
      } catch (error) {
        console.log("Помилка надсилання:", error);
      }
    }
  };

  const closeChat = async () => {
    try {
      await connection.stop();
    } catch (error) {
      console.log("Помилка при зупинці з’єднання:", error);
    }
    setConnection(null);
    setMessages([]);
    setChatRoom("");
    setUserName("");
  };

  return (
    <div className="min-h-screen flex items-center justify-center bg-gray-100">
      {connection ? (
        <Chat
          messages={messages}
          chatRoom={chatRoom}
          sendMessage={sendMessage}
          closeChat={closeChat}
          currentUser={userName} 
        />
      ) : (
        <WaitingRoom joinChat={joinChat} />
      )}
    </div>
  );
}

export default App;
