import { Button, CloseButton, Heading, Input } from "@chakra-ui/react";
import { Message } from "./Message";
import { useState, useRef, useEffect } from "react";

export const Chat = ({ messages, chatRoom, closeChat, sendMessage }) => {
  const [message, setMessage] = useState("");
  const messageEndRef = useRef();

  useEffect(() => {
    messageEndRef.current?.scrollIntoView({ behavior: "smooth" });
  }, [messages]);

  const onSendMessage = () => {
    if (message.trim() === "") return;
    sendMessage(message);
    setMessage("");
  };

  return (
    <div className="chat-wrapper">
      <div className="chat-header">
        <Heading size="md">{chatRoom}</Heading>
        <CloseButton onClick={closeChat} />
      </div>

      <div className="chat-messages">
        {messages.map((messageInfo, index) => (
          <Message messageInfo={messageInfo} key={index} />
        ))}
        <span ref={messageEndRef} />
      </div>

      <div className="chat-input">
        <Input
          type="text"
          value={message}
          onChange={(e) => setMessage(e.target.value)}
          placeholder="Введіть повідомлення"
        />
        <Button colorScheme="blue" onClick={onSendMessage}>
          Відправити
        </Button>
      </div>
    </div>
  );
};
