import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { DeliveryService } from '../delivery-service';

@Component({
  selector: 'app-delivery-address',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './delivery-address.html',
  styleUrl: './delivery-address.scss'
})
export class DeliveryAddress implements OnInit {
constructor(private es:DeliveryService){

}
ngOnInit(): void {
  this.FetchAddress();
}
fetchaddress:any;
FetchAddress(){
this.es.FetchDeliveryAddress().subscribe({
  next:res=>{
    console.log(res);
    this.fetchaddress=res;
  },
  error: err=>{
console.log(err.Message)
  }
})
}

}
