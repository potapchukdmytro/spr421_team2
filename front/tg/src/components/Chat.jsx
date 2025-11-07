import { Button, CloseButton, Heading, Input } from "@chakra-ui/react";
import { Message } from "./Message";
import { useState, useRef, useEffect } from "react";
import PropTypes from "prop-types";

export const Chat = ({ messages, chatRoom, closeChat, sendMessage, currentUser }) => {
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

  const handleKeyPress = (e) => {
    if (e.key === "Enter") {
      e.preventDefault();
      onSendMessage();
    }
  };

  return (
    <div className="app-bg flex items-center justify-center min-h-screen p-6">
      <div className="chat-wrapper">
        
        <div className="chat-header">
          <Heading size="md" color="#0088cc">
            {chatRoom}
          </Heading>
          <CloseButton onClick={closeChat} />
        </div>


        <div className="chat-messages">
          {messages.map((messageInfo, index) => (
            <Message
              key={index}
              messageInfo={messageInfo}
              currentUser={currentUser}
              
            />
          ))}
          <span ref={messageEndRef} />
        </div>


        <div className="chat-input">
          <Input
            type="text"
            value={message}
            onChange={(e) => setMessage(e.target.value)}
            placeholder="Введіть повідомлення"
            onKeyDown={handleKeyPress}
          />
          <Button
            colorScheme="telegram"
            onClick={onSendMessage}
            className="bg-[#0088cc] hover:bg-[#007ab8] text-white"
          >
            Відправити
          </Button>
        </div>
      </div>
    </div>
  );
};


Chat.propTypes = {
  messages: PropTypes.array.isRequired,
  chatRoom: PropTypes.string.isRequired,
  closeChat: PropTypes.func.isRequired,
  sendMessage: PropTypes.func.isRequired,
  currentUser: PropTypes.string.isRequired,
};
