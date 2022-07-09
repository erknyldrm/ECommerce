import { Directive, ElementRef, EventEmitter, HostListener, Input, Output, Renderer2 } from '@angular/core';
import { HttpClientService } from 'src/app/services/common/http-client.service';

@Directive({
  selector: '[appDelete]'
})
export class DeleteDirective {

  constructor(
    private element: ElementRef,
    private _renderer : Renderer2,
    private httpClientService: HttpClientService) {
      const img = _renderer.createElement("img");
      img.setAttribute("style","cursor:pointer");
      _renderer.appendChild(element.nativeElement,img);
    }

    @Input() id: string;
    @Input() controller: string;
    @Output() callback : EventEmitter<any> = new EventEmitter();

    @HostListener("click")
    async onClick(){
      this.httpClientService.delete({
          controller:this.controller
      },this.id).subscribe(data =>{
        //
      })
    }
}
