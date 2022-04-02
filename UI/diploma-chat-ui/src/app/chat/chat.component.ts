import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Component } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { CookieService } from "ngx-cookie-service";
import { ChatMember } from "src/Entities/ChatMember";

import * as signalR from "@aspnet/signalr";

@Component({
    selector: 'chat',
    templateUrl: './chat.component.html',
    styleUrls: ['./chat.component.scss']
})
export class ChatComponent {
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
        private cookieService: CookieService,
        router: Router,
        route: ActivatedRoute) {

        this.chatId = route.snapshot.params['chatId']
        this.userId = route.snapshot.params['userId']

        var authorizationToken = cookieService.get('AuthorizationToken');
        this.requestOptions = {
            headers: new HttpHeaders({
                'Authorization': `Bearer ${authorizationToken}`
            })
        }

        this.showChatDetails()
        this.getChatMembers()

        this.hubConnection = this.connectToChatHub()
        //this.hubConnection.send('SendMessage', this.userId, 'Test!')
    }

    showChatDetails() {
        this.httpClient
            .get(`https://localhost:44306/rooms/${this.chatId}/details`, this.requestOptions)
            .subscribe({
                next: (response: any) => { this.creatorNickname = response.creatorNickname },
                error: e => alert(JSON.stringify(e))
            })
    }

    getChatMembers() {
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
        Object.defineProperty(WebSocket, 'OPEN', { value: 1, });
        let hubConnection = new signalR.HubConnectionBuilder()
            .configureLogging(signalR.LogLevel.Trace)
            .withUrl('https://localhost:44373/ChatHub'
            // ,
            //     {
            //         skipNegotiation: true,
            //         transport: signalR.HttpTransportType.WebSockets
            //     }
            )
            .build();

        hubConnection.on('UserConnected', (input) => { alert(input) })
        hubConnection.on('UserDisconnected', (input) => { alert(input) })
        hubConnection.on('ReceiveMessage', (input) => { alert(input) })

        hubConnection
            .start()
            .then(() => {
                hubConnection.send('Connect', this.userId)
                    .then(() => { alert('done') })
                    .catch(err => alert(err))
            })
            .catch(err => alert('Error while starting connection: ' + err))

        return hubConnection
    }
}
