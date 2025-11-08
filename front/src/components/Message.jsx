export const Message = ({ messageInfo, currentUser }) => {
  const isOwnMessage = messageInfo.userName === currentUser;

  return (
    <div
      className={`flex flex-col ${
        isOwnMessage ? "items-end text-right" : "items-start text-left"
      }`}
    >
      {!isOwnMessage && (
        <span className="text-xs text-slate-600 mb-1 ml-1">
          {messageInfo.userName}
        </span>
      )}

      <div className={`msg ${isOwnMessage ? "msg-out" : "msg-in"}`}>
        {messageInfo.message}
      </div>
    </div>
  );
};
