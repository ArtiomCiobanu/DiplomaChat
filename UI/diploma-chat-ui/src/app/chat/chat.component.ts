import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Component, ViewChild, ViewContainerRef } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { CookieService } from "ngx-cookie-service";
import { ChatMember } from "src/Entities/ChatMember";

import * as signalR from "@aspnet/signalr";
import { ProperChatMessageComponent } from "./proper-chat-message/proper-chat-message.component";
import { ExternalChatMessageComponent } from "./external-chat-message/external-chat-message.component";

@Component({
    selector: 'chat',
    templateUrl: './chat.component.html',
    styleUrls: ['./chat.component.scss']
})
export class ChatComponent {
    @ViewChild('messageContainer', { read: ViewContainerRef }) messageContainer: ViewContainerRef;
    messages = [];

    creatorNickname: string;

    private chatId: string;
    private userId: string;

    private requestOptions: {
        headers: HttpHeaders
    }

    private chatMembers: ChatMember[]

    private hubConnection: signalR.HubConnection

    constructor(
        private httpClient: HttpClient,
        cookieService: CookieService,
        route: ActivatedRoute) {

        this.chatId = route.snapshot.params['chatId']

        this.userId = cookieService.get('userId')
        var authorizationToken = cookieService.get('AuthorizationToken');
        this.requestOptions = {
            headers: new HttpHeaders({
                'Authorization': `Bearer ${authorizationToken}`
            })
        }

        this.updateChatMembers()
        this.showChatDetails()

        this.hubConnection = this.connectToChatHub()
    }

    showChatDetails() {
        this.httpClient
            .get(`https://localhost:44306/rooms/${this.chatId}/details`, this.requestOptions)
            .subscribe({
                next: (response: any) => { this.creatorNickname = response.creatorNickname },
                error: e => alert(JSON.stringify(e))
            })
    }

    updateChatMembers() {
        this.httpClient
            .get(`https://localhost:44373/chatRooms/${this.chatId}/members`, this.requestOptions)
            .subscribe({
                next: (response: any) => {
                    this.chatMembers = response.members
                },
                error: e => alert(e)
            })
    }

    connectToChatHub(): signalR.HubConnection {
        let hubConnection = new signalR.HubConnectionBuilder()
            .configureLogging(signalR.LogLevel.Trace)
            .withUrl('https://localhost:44373/ChatHub',
                {
                    skipNegotiation: true,
                    transport: signalR.HttpTransportType.WebSockets
                }
            )
            .build();

        hubConnection.on('UserConnected', (userId, nickname) => this.UserConnected(userId, nickname))
        hubConnection.on('UserDisconnected', (input) => this.userDisconnected(input))
        hubConnection.on('ReceiveMessage', (userId, message) => this.receiveMessage(userId, message))

        hubConnection
            .start()
            .then(() => {
                this.hubConnection.invoke(
                    'Connect',
                    this.userId);
            })
            .catch(err => alert('Error while starting connection: ' + err))

        return hubConnection
    }

    UserConnected(userId: string, nickname: string) {
        this.chatMembers.push(new ChatMember(userId, nickname))

        this.addExternalMessage(`${nickname} has joined the chat.`, 'System')
    }

    userDisconnected(user: any) {
        alert(user)
    }

    receiveMessage(userId: string, message: string) {
        if (userId == this.userId) {
            let chatMessage = this.messageContainer.createComponent(ProperChatMessageComponent).instance
            chatMessage.text = message
        }
        else {
            this.chatMembers.forEach((member: any) => {
                if (member.userId == userId) {
                    this.addExternalMessage(message, member.nickname)
                }
            });
        }
    }

    sendMessageToChat(message: string) {
        this.hubConnection.send('SendMessage', this.userId, message)
    }

    addExternalMessage(text: string, sender: string) {
        let chatMessage = this.messageContainer.createComponent(ExternalChatMessageComponent).instance
        chatMessage.text = text
        chatMessage.sender = sender
    }
}
