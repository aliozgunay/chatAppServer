using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fleck;
using Newtonsoft.Json.Linq;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
namespace chatAppServer
{
    class Program
    {

        public class GlobalDB
        {
            private static string connectionString = @"Data Source=10.10.10.211\SqlExpress;Initial Catalog=StajyerV5;User ID=sa;Password=AMAZ10;MultipleActiveResultSets=True";
            private static SqlConnection cnn = new SqlConnection(connectionString);
            public static string ConnStr
            {
                get { return connectionString; }
                set {  }
            }

            public static SqlConnection currDb
            {
                get {
                    if(cnn.State == System.Data.ConnectionState.Closed)
                    {
                        cnn.Open();
                    }
                    return cnn;
                }
            }
        }



        static void Main(string[] args)
        {
            string generateID()
            {
                return Guid.NewGuid().ToString("N");
            }
            List<IWebSocketConnection> allSockets = new List<IWebSocketConnection>();
            void sendSocketMessage(string peerID, string client, string message, string senderUsername, int senderID = 0)
            {

                foreach (IWebSocketConnection socketConnection in allSockets)
                {
                    if (socketConnection.ConnectionInfo.Id.ToString() == client)
                    {
                        dynamic returnedValue = new JObject();
                        returnedValue.function = "receiveMessage";
                        returnedValue.message = message;
                        returnedValue.senderID = senderID;
                        returnedValue.fromUname = senderUsername;
                        returnedValue.uniq = generateID();
                        socketConnection.Send(returnedValue.ToString().Trim());
                        Console.WriteLine(peerID + " is sending to " + client + " a message : " + message);
                    }
                }
            }

            void sendSocketMessageToGroup(string peerID, string client, string message, string senderUsername, int senderID, string groupName)
            {

                foreach (IWebSocketConnection socketConnection in allSockets)
                {
                    if (socketConnection.ConnectionInfo.Id.ToString() == client)
                    {
                        dynamic returnedValue = new JObject();
                        returnedValue.function = "receiveMessageGroup";
                        returnedValue.message = message;
                        returnedValue.groupID = senderID;
                        returnedValue.uniq = generateID();
                        returnedValue.fromUname = senderUsername;
                        returnedValue.groupName = groupName;
                        socketConnection.Send(returnedValue.ToString().Trim());
                        Console.WriteLine(peerID + " is sending to " + client + " a message on an group: " + message);
                    }
                }
            }

            void sendSocket(string peerID, string client, string message, int senderID = 0)
            {

                foreach (IWebSocketConnection socketConnection in allSockets)
                {
                    if (socketConnection.ConnectionInfo.Id.ToString() == client)
                    {
                        
                        socketConnection.Send(message);
                        Console.WriteLine(peerID + " is sending to " + client + " a message : " + message);
                    }
                }
            }
            void sendToAllClients(string connector)
            {
                foreach (IWebSocketConnection socketConnection in allSockets)
                {
                    
                        //socketConnection.Send("New Connector : " + connector);
                  
                }

            }
            var server = new WebSocketServer("ws://10.10.10.57:8181");
            void sendOnlineUsers(IWebSocketConnection socket, string username, int uID)
            {

               
               
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;                
                cmd.Connection = GlobalDB.currDb;
                cmd.CommandText = "onlineUsers";
                cmd.Parameters.AddWithValue("username", username);
                SqlDataReader readIt = cmd.ExecuteReader();
                dynamic returnedValue = new JObject();
                returnedValue.function = "getOnlineUsers";
                dynamic userListArray = new JArray();
                dynamic clienListArray = new JArray();
                dynamic messageInformation = new JArray();

                while (readIt.Read())
                {

                        SqlCommand checkConv = new SqlCommand();
                        checkConv.CommandType = System.Data.CommandType.StoredProcedure;
                        checkConv.Connection = GlobalDB.currDb;
                        checkConv.CommandText = "getConvInfoByMe";
                        checkConv.Parameters.AddWithValue("uID", uID);
                        checkConv.Parameters.AddWithValue("fID", Int16.Parse(readIt["U_ID"].ToString()));
                        SqlDataReader readConvInfo = checkConv.ExecuteReader();
                        
                        userListArray.Add(readIt["Username"].ToString());
                        clienListArray.Add(readIt["U_ID"].ToString());
                        if (readConvInfo.Read())
                        {
                            messageInformation.Add(readConvInfo["toplam"].ToString());
                            
                        }



                }
               
                returnedValue.listUser = userListArray;
                returnedValue.listClient = clienListArray;
                returnedValue.convClient = messageInformation;


                Console.WriteLine("List is sended");
                socket.Send(returnedValue.ToString().Trim());
                

            }

            void sendUsers(IWebSocketConnection socket, string username, int uID)
            {

               
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = GlobalDB.currDb;
                cmd.CommandText = "allUsers";
                cmd.Parameters.AddWithValue("username", username);
                SqlDataReader readIt = cmd.ExecuteReader();
                dynamic returnedValue = new JObject();
                returnedValue.function = "getUsers";
                dynamic userListArray = new JArray();
                dynamic clienListArray = new JArray();
                while(readIt.Read())
                {
                    userListArray.Add(readIt["Username"].ToString());
                    clienListArray.Add(readIt["U_ID"].ToString());
                }


               
                returnedValue.listUser = userListArray;
                returnedValue.listClient = clienListArray;
              


                Console.WriteLine("UserList is sended");
                socket.Send(returnedValue.ToString().Trim());


            }

            void getGroups(IWebSocketConnection socket, int uID)
            {

               
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = GlobalDB.currDb;
                cmd.CommandText = "getGroups";
                cmd.Parameters.AddWithValue("uID", uID);
                SqlDataReader readIt = cmd.ExecuteReader();
                dynamic returnedValue = new JObject();
                returnedValue.function = "getMyGroups";
                dynamic groupListArray = new JArray();
                dynamic groupIDListArray = new JArray();
                dynamic messageInformation = new JArray();

                while (readIt.Read())
                {



                    groupListArray.Add(readIt["Group_Name"].ToString());
                    groupIDListArray.Add(readIt["group_conversation_id"].ToString());


                }
               
                returnedValue.listGroup = groupListArray;
                returnedValue.listGroupID = groupIDListArray;


                Console.WriteLine("Group List is sended");
                socket.Send(returnedValue.ToString().Trim());


            }

            bool checkConversation(string pID, string cID)
            {
               
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = GlobalDB.currDb;
                cmd.CommandText = "checkConversation";
                cmd.Parameters.AddWithValue("@c_from", pID);
                cmd.Parameters.AddWithValue("@c_to", cID);
                SqlDataReader readIt = cmd.ExecuteReader();
                if (readIt.Read())
                {
                   
                    return true;
                }
                else
                {
                   
                    return false;
                }
            }

            int getConversationIDByUID(int pID, int cID)
            {
               
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = GlobalDB.currDb;
                cmd.CommandText = "getConversationByUID";
                cmd.Parameters.AddWithValue("@c_from", pID);
                cmd.Parameters.AddWithValue("@c_to", cID);
                SqlDataReader readIt = cmd.ExecuteReader();
                if (readIt.Read())
                {

                    return short.Parse(readIt[0].ToString());
                }
                else
                {
                    return 0;
                }
            }
            int getConversationID(string pID, string cID)
            {
               
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = GlobalDB.currDb;
                cmd.CommandText = "getConversation";
                cmd.Parameters.AddWithValue("@c_from", pID);
                cmd.Parameters.AddWithValue("@c_to", cID);
                SqlDataReader readIt = cmd.ExecuteReader();
                if (readIt.Read())
                {
                    
                    return short.Parse(readIt[0].ToString());
                }
                else
                {
                    return 0;
                }
            }

            bool addConversation(string peerID, string clientID)
            {
               
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = GlobalDB.currDb;
                cmd.CommandText = "Addconversation";
                cmd.Parameters.AddWithValue("@c_from", peerID);
                cmd.Parameters.AddWithValue("@c_to", clientID);
                cmd.ExecuteNonQuery();
               
                return true;

            }

            void getChatHistory(string peerID, int myID, int client)
            {
                int convID = getConversationID(peerID, client.ToString());
               
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = GlobalDB.currDb;
                cmd.CommandText = "getConvMessages";
                cmd.Parameters.AddWithValue("@convID", convID);
                SqlDataReader reader = cmd.ExecuteReader();
                dynamic returnedValue = new JObject();
                returnedValue.function = "loadHistory";
                dynamic messageList = new JArray();
                
                while (reader.Read())
                {
                    dynamic messageInfo = new JArray();
                    messageInfo.Add(reader["message"].ToString());
                    messageInfo.Add(reader["uname"].ToString());
                    messageInfo.Add(reader["m_time"].ToString());




                    messageList.Add(messageInfo);
                }
               
                returnedValue.messages = messageList;
                Console.WriteLine(getClientID(client));
                sendSocket(peerID, peerID, returnedValue.ToString().Trim());
            }


            void getChatHistoryGroup(string peerID, int myID, int gid)
            {
               
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = GlobalDB.currDb;
                cmd.CommandText = "getGroupConvMessages";
                cmd.Parameters.AddWithValue("@gID", gid);
                SqlDataReader reader = cmd.ExecuteReader();
                dynamic returnedValue = new JObject();
                returnedValue.function = "loadHistoryGroup";
                dynamic messageList = new JArray();

                while (reader.Read())
                {
                    dynamic messageInfo = new JArray();
                    messageInfo.Add(reader["gmessage"].ToString());
                    messageInfo.Add(reader["uname"].ToString());

                    messageList.Add(messageInfo);
                }
               
                returnedValue.messages = messageList;
                sendSocket(peerID, peerID, returnedValue.ToString().Trim());
            }

            string getClientID(int uID)
            {
               
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = GlobalDB.currDb;
                cmd.CommandText = "getClientID";
                cmd.Parameters.AddWithValue("@cID", uID);
                SqlDataReader oku = cmd.ExecuteReader();
                if (oku.Read())
                {
                    return oku["clientID"].ToString();
                }
                else
                {
                    return "";
                }
            }

            void addMessage(string peerID, string clientID, string message, int convID, int senderID, string senderUsername)
            {
               
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = GlobalDB.currDb;
                cmd.CommandText = "addMessage";
                cmd.Parameters.AddWithValue("@m_from", peerID);
                cmd.Parameters.AddWithValue("@m_to", clientID);
                cmd.Parameters.AddWithValue("@message", message);
                cmd.Parameters.AddWithValue("@conversation_id", convID);
                cmd.ExecuteNonQuery();
                clientID = getClientID(Int16.Parse(clientID));
                sendSocketMessage(peerID, clientID, message, senderUsername, senderID) ;
               
            }
            void addMessageGroup(string peerID, string message, int convID, int senderID, string senderUsername, string groupName)
            {
               
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = GlobalDB.currDb;
                cmd.CommandText = "addMessageGroup";
                cmd.Parameters.AddWithValue("@m_from", peerID);
                cmd.Parameters.AddWithValue("@message", message);
                cmd.Parameters.AddWithValue("@conversation_id", convID);
                cmd.ExecuteNonQuery();
                SqlCommand cmdUsers = new SqlCommand();

                cmdUsers.CommandType = System.Data.CommandType.StoredProcedure;
                cmdUsers.Connection = GlobalDB.currDb;
                cmdUsers.CommandText = "getUsersFromGroup";
                cmdUsers.Parameters.AddWithValue("@groupID", convID);
                SqlDataReader getUsers = cmdUsers.ExecuteReader();
                while (getUsers.Read())
                {
                    string clientID = getClientID(Int16.Parse(getUsers["U_ID"].ToString()));
                    sendSocketMessageToGroup(peerID, clientID, message, senderUsername, convID, groupName);
                }
               

            }
            void disconnectFromDbSocket(string socketid)
            {
               
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = GlobalDB.currDb;
                cmd.CommandText = "disconnectUser";
                cmd.Parameters.AddWithValue("@cid", socketid);
                cmd.ExecuteNonQuery();
               
            }

            void readAllMessages(int uID, int cID)
            {
                int convId = getConversationIDByUID(uID, cID);
               
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = GlobalDB.currDb;
                cmd.CommandText = "readAllMessages";
                cmd.Parameters.AddWithValue("@cid", convId);
                cmd.Parameters.AddWithValue("@uid", uID);
                cmd.ExecuteNonQuery();
               
                Console.WriteLine(uID.ToString() + " IS READ ALL MESSAGES FROM CONVERSATION");
            }

            void createGroup(int creatorID, string groupName)
            {

                SqlCommand cmdCreatGroup = new SqlCommand();
                cmdCreatGroup.CommandType = System.Data.CommandType.StoredProcedure;
                cmdCreatGroup.Connection = GlobalDB.currDb;
                cmdCreatGroup.CommandText = "groupCheck";

                cmdCreatGroup.Parameters.AddWithValue("@Group_Name", groupName);
                cmdCreatGroup.Parameters.AddWithValue("@creatorID", creatorID);
                SqlDataReader groupRead = cmdCreatGroup.ExecuteReader();
                if (groupRead.Read())
                {
                    dynamic returnedValue = new JObject();
                    returnedValue.function = "creatGroup";
                    returnedValue.code = "success";

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Connection = GlobalDB.currDb;
                    cmd.CommandText = "createGroup";
                    cmd.Parameters.AddWithValue("@creatorID", creatorID);
                    cmd.Parameters.AddWithValue("@groupName", groupName);
                    cmd.ExecuteNonQuery();


                }
                Console.WriteLine("success");









                
            }



            int getGroupID(string groupName)
            {
               
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Connection = GlobalDB.currDb;
                cmd.CommandText = "getGroupID";
                cmd.Parameters.AddWithValue("@groupName", groupName);
                SqlDataReader readGroupID = cmd.ExecuteReader();
                if (readGroupID.Read())
                {
                    return Int16.Parse(readGroupID["group_conversation_id"].ToString());
                }
                else{
                    return 0;
                }
            }

            server.Start(socket =>
            {
                string peerID = socket.ConnectionInfo.Id.ToString();
                socket.OnOpen = () => {
                    Console.WriteLine(allSockets.Count);
                    allSockets.Add(socket);
                    Console.WriteLine(peerID + " is connected.");
                    sendToAllClients(peerID);

                };
                socket.OnClose = () =>
                {
                    allSockets.Remove(socket);
                    disconnectFromDbSocket(socket.ConnectionInfo.Id.ToString());
;                };
                socket.OnMessage = (message) =>
                {
                    if(message.Length > 0)
                    {
                       
                        dynamic jsonParsed = JObject.Parse(message);
                            Console.WriteLine(jsonParsed.function.ToString());
                        switch (jsonParsed.function.ToString())
                        {

                            case "readMessages":
                                
                                int readerID = Int16.Parse(jsonParsed.uid.ToString());
                                int readedID = Int16.Parse(jsonParsed.client.ToString());
                                readAllMessages(readerID, readedID);
                                break;
                            case "getOnlineUsers":
                                int myuID = Int16.Parse(jsonParsed.uid.ToString());
                                string myUsername = jsonParsed.username.ToString();
                                sendOnlineUsers(socket, myUsername, myuID);
                                Console.WriteLine("Request is Arrived To Server");
                                break;
                            case "getUsers":
                                int myuIDgrp = Int16.Parse(jsonParsed.uid.ToString());
                                string myUsernamegrp = jsonParsed.username.ToString();
                                sendUsers(socket, myUsernamegrp, myuIDgrp);
                                Console.WriteLine("Request is Arrived To Server");
                                break;
                            case "getGroups":
                                Console.WriteLine(jsonParsed.function.ToString());
                                int myuID4Groups = Int16.Parse(jsonParsed.uid.ToString());
                                getGroups(socket, myuID4Groups);
                                Console.WriteLine("GroupList Request is Arrived To Server");
                               break;
                            case "getHistory":
                                int myID = Int16.Parse(jsonParsed.myUid.ToString());
                                int toID = Int16.Parse(jsonParsed.client.ToString());
                                getChatHistory(peerID, myID, toID);
                                break;
                            case "getHistoryGroup":
                                int myUserId = Int16.Parse(jsonParsed.myUid.ToString());
                                string groupName = jsonParsed.groupName.ToString();
                                int groupIDHistory = getGroupID(groupName);
                                getChatHistoryGroup(peerID, myUserId, groupIDHistory);
                                break;
                            case "createGroup":
                                int creatorID = Int16.Parse(jsonParsed.myid.ToString());
                                string cgroupName = jsonParsed.groupName.ToString();

                                createGroup(creatorID, cgroupName);

                                
                                break;
                            case "sendMessage":
                                string clientID = jsonParsed.client.ToString();
                                string senderUsername = jsonParsed.username.ToString();
                                Console.WriteLine(senderUsername + " is sending a message to ; " + clientID);

                                if(checkConversation(peerID, clientID))
                                {
                                    int sendMessageconvID = getConversationID(peerID, clientID);
                                    addMessage(peerID, clientID, jsonParsed.message, sendMessageconvID, jsonParsed.myUid, senderUsername) ;
                                }
                                else
                                {

                                    if (addConversation(peerID, clientID))
                                    {
                                        int sendMessageconvID = getConversationID(peerID, clientID);
                                        addMessage(peerID, clientID, jsonParsed.message, sendMessageconvID, jsonParsed.myUid, senderUsername);
                                    }
                                }

                                break;
                                case "sendMessageGroup":
                                string receiverGroupName = jsonParsed.groupName.ToString();
                                string senderGroupUsername = jsonParsed.username.ToString();
                                Console.WriteLine(senderGroupUsername + " is sending a message to ; " + receiverGroupName);

                                int convID = getGroupID(receiverGroupName);
                                addMessageGroup(peerID, jsonParsed.message, convID, jsonParsed.myUid, senderGroupUsername, receiverGroupName);
                                  

                                break;
                                case "syncID":
                                
                                SqlCommand cmd = new SqlCommand();
                                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                                cmd.Connection = GlobalDB.currDb;
                                cmd.CommandText = "syncID";

                                cmd.Parameters.AddWithValue("@uid", jsonParsed.id.ToString());
                                cmd.Parameters.AddWithValue("@cid", (peerID));
                                cmd.ExecuteNonQuery();
                                Console.WriteLine(jsonParsed.id.ToString() + " is connected with "+peerID);


                                break;
                            case "login":

                                string username, password;
                                username = jsonParsed.username.ToString();
                                password = jsonParsed.password.ToString();
                                
                                SqlCommand cmdLogin = new SqlCommand();
                                cmdLogin.CommandType = System.Data.CommandType.StoredProcedure;
                                cmdLogin.Connection = GlobalDB.currDb;
                                cmdLogin.CommandText = "userLogin";

                                cmdLogin.Parameters.AddWithValue("@username", username);
                                cmdLogin.Parameters.AddWithValue("@password", password);

                                SqlDataReader readLogin = cmdLogin.ExecuteReader();
                                if (readLogin.Read())
                                {
                                    Console.WriteLine("Logged In");
                                    dynamic returnedValue = new JObject();
                                    returnedValue.returned = "true";
                                    returnedValue.userID = readLogin["U_ID"];
                                    returnedValue.username = readLogin["Username"];
                                    sendSocket(peerID, peerID, returnedValue.ToString().Trim());
                                }
                                else
                                {
                                    dynamic returnedValue = new JObject();
                                    returnedValue.returned = "false";
                                    sendSocket(peerID, peerID, returnedValue.ToString().Trim());

                                }



                                break;
                            case "signup":
                                string  email, susername, spassword;
                                email = jsonParsed.email.ToString();
                                susername = jsonParsed.username.ToString();
                                spassword = jsonParsed.password.ToString();

                                SqlCommand cmdSignup = new SqlCommand();
                                cmdSignup.CommandType = System.Data.CommandType.StoredProcedure;
                                cmdSignup.Connection = GlobalDB.currDb;
                                cmdSignup.CommandText = "Reg_Check";

                                cmdSignup.Parameters.AddWithValue("@email", email);
                                cmdSignup.Parameters.AddWithValue("@username", susername);
                                cmdSignup.Parameters.AddWithValue("@password", spassword);
                                SqlDataReader signRead = cmdSignup.ExecuteReader();
                                if (signRead.Read())
                                {
                                        dynamic returnedValue = new JObject();
                                        returnedValue.function = "signupReturn";
                                        returnedValue.code = "success";
                                        sendSocket(peerID, peerID, returnedValue.ToString());
                                 
                                }
                                Console.WriteLine("signupsucc");

                                break;


                            default:
                                break;
                        }
                        
                    }
                };
            });


            
            

            Console.ReadLine();
        }
    }
}
