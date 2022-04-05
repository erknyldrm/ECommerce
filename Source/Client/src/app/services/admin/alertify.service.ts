import { Injectable } from '@angular/core';
declare var alertify: any;

@Injectable({
  providedIn: 'root',
})
export class AlertifyService {
  constructor() {}

  message(message : string, alertifyOptions : Partial<AlertifyOptions>) {

    alertify.set('notifier', 'delay', alertifyOptions.delay);
    alertify.set('notifier','position', alertifyOptions.position);
    const msj = alertify[alertifyOptions.messageType](message);

    if(alertifyOptions.dismissOthers)
      msj.dismissOthers();
  }

  dismiss() {
    alertify.dismissAll();
  }
}

export class AlertifyOptions {
  messageType : MessageType = MessageType.Message;
  position : Position = Position.BottomLeft;
  delay : number = 3;
  dismissOthers: boolean = false;
}

export enum MessageType {
  Error = 'error',
  Message = 'message',
  Notify = 'notify',
  Success = 'success',
  Warning = 'warning',
}

export enum Position {
  TopCenter = 'top-center',
  TopRight = 'top-right',
  TopLeft = 'top-left',
  BottomLeft = 'bottom-left',
  BottomRight = 'bottom-right',
  BottomCenter = 'bottom-center',
}
