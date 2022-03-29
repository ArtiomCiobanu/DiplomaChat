import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Component, ComponentFactoryResolver, ViewChild, ViewContainerRef } from "@angular/core";
import { Router } from "@angular/router";
import { CookieService } from "ngx-cookie-service";
import { ChatListElementComponent } from "./chat-list-element/chat-list-element.component";

@Component({
    selector: 'chat-list',
    templateUrl: './chat-list.component.html',
    styleUrls: ['./chat-list.component.scss']
})
export class ChatListComponent {
    @ViewChild('chatContainer', { read: ViewContainerRef }) chatContainer: ViewContainerRef;
    chats = [];

    nickname: string = ""

    requestOptions: {
        headers: HttpHeaders
    }

    constructor(
        private httpClient: HttpClient,
        private router: Router,
        private cookieService: CookieService) {

        this.nickname = cookieService.get('Nickname')

        var authorizationToken = this.cookieService.get('AuthorizationToken');

        this.requestOptions = {
            headers: new HttpHeaders({
                'Authorization': `Bearer ${authorizationToken}`
            })
        }

        this.loadChats()
        this.leaveAllChatRooms()
    }

    loadChats() {
        var offset = 0;
        var limit = 10;

        this.httpClient
            .get(`https://localhost:44306/rooms/created?offset=${offset}&limit=${limit}`, this.requestOptions)
            .subscribe({
                next: response => this.chatListReceived(response)
            })
    }

    logout() {
        this.cookieService.deleteAll();

        this.router.navigate(['']);
    }

    createNewChat() {
        this.httpClient
            .get("https://localhost:44306/rooms/create", this.requestOptions)
            .subscribe({
                next: response => this.successfullyCreated(response),
                error: e => alert(e)
            });
    }

    successfullyCreated(response: any) {
        this.router.navigate([`chats/${response.sessionId}`]);
    }

    chatListReceived(response: any) {
        for (let i = 0; i < response.chatRooms.length; i++) {
            var chatRoom = response.chatRooms[i]
            this.addChatToList(`Chat ${(i + 1).toString()}`, chatRoom.id, chatRoom.creatorNickname, chatRoom.userAmount)
        }
    }

    addChatToList(title: string, roomId: string, creatorNickname: string, userAmount: number) {
        var chatListElement = this.chatContainer.createComponent(ChatListElementComponent).instance

        chatListElement.Title = title
        chatListElement.CreatorNickname = creatorNickname
        chatListElement.RoomId = roomId
    }

    leaveAllChatRooms() {
        this.httpClient
            .get(`https://localhost:44306/rooms/leave-all`, this.requestOptions)
            .subscribe({
                error: e => alert(JSON.stringify(e))
            })
    }
}
