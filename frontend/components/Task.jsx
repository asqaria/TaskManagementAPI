import React, {useEffect, useState} from 'react'
import * as signalR from "@microsoft/signalr"
import { title } from 'process';

const Task = () => {
    const [connection, setConnection] = useState(null);
    const [messages, setMessages] = useState([]);
    
    useEffect(() => {
        const newConnection = new signalR.HubConnectionBuilder()
            .withUrl("http://localhost:5001/taskHub")
            .withAutomaticReconnect()
            .build();

        newConnection
            .start()
            .then(() => console.log("Connected to SignalR"))
            .catch((err) => console.error("Connection Failed: ", err));
        
        newConnection.on("RecieveMessage", (title, description, status) => {
            setMessages((prev) => [...prev, {title, description, status}]);
        })

        setConnection(newConnection);

        return () => {
            newConnection.stop();
        }
    }, []);

    const sendMessage = async () => {
        if(connection){
            await connection.invoke("SendMessage", title, description, status);
            setMessage("");
        }
    }

    return (
        <div>
                {messages.map((msg, index) => (
                    <div className='m-6 mx-auto flex max-w-sm items-center gap-x-4 rounded-xl bg-white p-6 shadow-lg outline outline-black/5 dark:bg-slate-800 dark:shadow-none dark:-outline-offset-1 dark:outline-white/10'>
                        <div>
                        <div key={index} className='text-xl font-medium text-black dark:text-white'>
                        {msg.title}
                        </div>
                        <p className='text-gray-500 dark:text-gray-400'>
                            {msg.description}
                        </p>
                        <p className='text-[12px]'>
                            Status: {msg.status}
                        </p>
                        </div>
                    </div>
                ))}
        </div>
    )
}

export default Task