import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class CustomToastrService {

  constructor(private toastr: ToastrService) {
  }

  message(message:string, title: string, toastrOptions: Partial<ToastrOptions>) {
    this.toastr[toastrOptions.messageType](message,title, {
      positionClass: toastrOptions.position
    });
  }
}

export enum ToastrMessageType {
  Error = 'error',
  Message = 'message',
  Notify = 'notify',
  Success = 'success',
  Warning = 'warning',
}

export enum ToastrPosition {
  TopCenter = 'toast-top-center',
  TopRight = 'toast-top-right',
  TopLeft = 'toast-top-left',
  BottomLeft = 'toast-bottom-left',
  BottomRight = 'toast-bottom-right',
  BottomCenter = 'toast-bottom-center',
}

export class ToastrOptions {
messageType: ToastrMessageType;
position: ToastrPosition;
}

