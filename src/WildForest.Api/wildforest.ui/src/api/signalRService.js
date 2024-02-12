import * as signalR from "@microsoft/signalr";

export class SignalRService {
    constructor(url = "/chatHub", onSuccess, onError) {
        this.connection = new signalR()
            .HubConnectionBuilder()
            .withUrl(url)
            .build();

        this.#start(onSuccess, onError);
    }

    onMethodExecuted(methodName, action) {
        this.connection.on(methodName, function(data) {
            action(data);
        });
    }

    send(methodName, data, onError) {
        this.connection.invoke(methodName, data)
            .catch(function(err) {
                onError();
            });
    }

    #start(onSuccess, onError) {
        this.connection.start()
            .then(function() {
                onSuccess();
            })
            .catch(function(err) {
                onError();
            });
    }
}