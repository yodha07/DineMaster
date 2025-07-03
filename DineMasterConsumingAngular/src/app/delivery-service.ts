import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';


@Injectable({
  providedIn: 'root'
})
export class DeliveryService {

  constructor(private http: HttpClient) { }
  url="https://localhost:7292/api/Delivery/GetAll";

  FetchDeliveryAddress(){
    return this.http.get(this.url);
  }

  url2="https://localhost:7292/api/Delivery/AddDeliveryAddress";

  AddDeliveryAddress(data:any){
return this.http.post(this.url2, data)
  }

  url3="https://localhost:7292/api/Delivery/Delete/";
  
  deleteAddress(id:any){
    return this.http.delete(this.url3+id)
  }

  url4="https://localhost:7292/api/Delivery/GetById/";
  getDeliveryAddId(id:any){
    return this.http.get(this.url4+id)
  }

  url5="https://localhost:7292/api/Delivery/Update";
  updateDeliveryAddress(data:any, id:any){
    return this.http.put(this.url5+id, data)
  }

  urlStatus = "https://localhost:7292/api/Delivery/status/";
  updateStatus(orderId: number, status: string) {
    return this.http.put(this.urlStatus + orderId + "?status=" + status, null);
  }

  
  urlLatestStatus = "https://localhost:7292/api/Delivery/status/latest/";
  getLatestStatus(orderId: number) {
    return this.http.get(this.urlLatestStatus + orderId);
  }

 
  urlVerifyOtp = "https://localhost:7292/api/Delivery/verify-delivery";
  verifyOtp(data: any) {
    return this.http.post(this.urlVerifyOtp, data);
  }
}
