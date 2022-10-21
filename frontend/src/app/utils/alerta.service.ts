import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';


@Injectable({
  providedIn: 'root'
})
export class AlertaService {
  textoSpinner: string = '';
  constructor() { }

  alertError(text: string, callBack?: any): boolean {
    Swal.fire({
      title: 'Error',
      html: text,
      icon: 'error',
      allowOutsideClick: false,
      allowEscapeKey: false,
      showCancelButton: true,
      showConfirmButton: false,
      cancelButtonColor: '#d33',
      cancelButtonText: 'Ok',
    }).then((value) => {
      if (callBack)
        callBack();
    });
    return false;
  }

  alertConfirm(title: string = '', text: string = '', callBackOk?: any, callBackError?: any) {
    Swal.fire({
      title: title,
      html: text,
      icon: 'question',
      allowOutsideClick: false,
      allowEscapeKey: false,
      showCancelButton: true,
      cancelButtonColor: '#b5b3b3',
      cancelButtonText: 'No',
      confirmButtonText: 'Sí',
      reverseButtons: false
    }).then((resultado) => {
      if (resultado.value) {
        if (callBackOk)
          callBackOk();
      }
      else
        if (callBackError)
          callBackError();
    });
  }

  alertOk(title: string = '', text: string = '', callBack?: any) {
    Swal.fire({
      title: title,
      icon: 'success',
      html: text,
      allowOutsideClick: false,
      allowEscapeKey: false,
    }).then(resultado => {
      if (callBack)
        callBack();
    });
  }

  alertBasic(text: string, callBack?: any) {
    Swal.fire({
      text: text,
      allowOutsideClick: false,
      allowEscapeKey: false,
    }).then(resultado => {
      if (callBack)
        callBack();
    });
  }
}
