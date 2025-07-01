import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class DeliveryService {

  constructor(private http: HttpClient) { }
  url="http://localhost:7292/api/Delivery/GetAll";

  FetchDeliveryAddress(){
    return this.http.get(this.url);
  }
}
