import { Button, Heading, Input, Text } from "@chakra-ui/react";
import { useState } from "react";

export const WaitingRoom = ({ joinChat }) => {
  const [userName, setUserName] = useState("");
  const [chatRoom, setChatRoom] = useState("");

  const onSubmit = (e) => {
    e.preventDefault();
    joinChat(userName, chatRoom);
  };

  return (
    <div className="form-wrapper">
      <Heading className="text-[#0088cc] text-center mb-6">Онлайн чат</Heading>
      <form onSubmit={onSubmit}>
        <div className="mb-4">
          <Text fontSize="sm" className="text-gray-600 font-medium mb-1 block">
            Ім’я користувача
          </Text>
          <Input
            type="text"
            value={userName}
            onChange={(e) => setUserName(e.target.value)}
            placeholder="Введіть ваше ім’я"
            className="focus:border-[#0088cc] bg-gray-50"
            required
          />
        </div>
        <div className="mb-6">
          <Text fontSize="sm" className="text-gray-600 font-medium mb-1 block">
            Назва чату
          </Text>
          <Input
            type="text"
            value={chatRoom}
            onChange={(e) => setChatRoom(e.target.value)}
            placeholder="Введіть назву чату"
            className="focus:border-[#0088cc] bg-gray-50"
            required
          />
        </div>
        <Button
          type="submit"
          className="w-full py-2 bg-[#0088cc] hover:bg-[#007ab8] text-white font-medium rounded-lg shadow-md transition"
        >
          Приєднатися до чату
        </Button>
      </form>
    </div>
  );
};
