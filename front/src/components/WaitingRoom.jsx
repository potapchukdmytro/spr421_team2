import { Button, Heading, Input, Text } from "@chakra-ui/react";
import { useState } from "react";
import PropTypes from "prop-types";
import "../index.css"; 

const WaitingRoom = ({ joinChat }) => {
  const [userName, setUserName] = useState("");
  const [chatRoom, setChatRoom] = useState("");

  const onSubmit = (e) => {
    e.preventDefault();
    joinChat(userName.trim(), chatRoom.trim());
  };

  return (
    <div className="app-bg flex items-center justify-center min-h-screen p-6">
      <div className="waiting-card w-full max-w-md p-8 rounded-2xl shadow-xl border border-gray-200">
        <Heading size="lg" mb={6} className="text-[#0088cc] text-center">
          Онлайн чат
        </Heading>

        <form onSubmit={onSubmit}>
          <div className="mb-5">
            <Text fontSize="sm" className="text-gray-600 font-medium mb-2 block">
              Ім’я користувача
            </Text>
            <Input
              type="text"
              value={userName}
              onChange={(e) => setUserName(e.target.value)}
              placeholder="Введіть ваше ім’я"
              className="bg-gray-50 focus:border-[#0088cc]"
              required
            />
          </div>

          <div className="mb-6">
            <Text fontSize="sm" className="text-gray-600 font-medium mb-2 block">
              Назва чату
            </Text>
            <Input
              type="text"
              value={chatRoom}
              onChange={(e) => setChatRoom(e.target.value)}
              placeholder="Введіть назву чату"
              className="bg-gray-50 focus:border-[#0088cc]"
              required
            />
          </div>

          <Button
            type="submit"
            colorScheme="telegram"
            className="w-full py-2 bg-[#0088cc] hover:bg-[#007ab8] text-white font-medium rounded-lg shadow-md transition"
          >
            Приєднатися до чату
          </Button>
        </form>
      </div>
    </div>
  );
};

WaitingRoom.propTypes = {
  joinChat: PropTypes.func.isRequired,
};

export default WaitingRoom;
