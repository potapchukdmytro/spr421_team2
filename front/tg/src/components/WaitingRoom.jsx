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
        <form 
        onSubmit={onSubmit}
        className="max-w-md w-full bg-white p-8 rounded shadow-lg">
            <Heading>Онлайн чат</Heading>
            <div className="mb-4">
                <Text fontSize={"sm"}>Ім'я користувача</Text>
                <Input onChange={(e) => setUserName(e.target.value)} 
                name="userName" 
                placeholder="Введіть ваше ім'я" />
            </div>
            <div className="mb-4">
                <Text fontSize={"sm"}>Назва чату</Text>
                <Input 
                onChange={(e) => setChatRoom(e.target.value)}
                name="chatRoom" 
                placeholder="Введіть назву чата" />
            </div>
            <Button type="submit" colorScheme="blue" >
                Приєднатися до чату
            </Button>
        </form>
    );
}