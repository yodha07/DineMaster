import { Component, OnInit } from '@angular/core';
import { DeliveryService } from '../delivery-service';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
// import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-delivery-track',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './delivery-track.html',
  styleUrl: './delivery-track.scss'
})
export class DeliveryTrack implements OnInit {
  constructor(private ds:DeliveryService) {}

  ngOnInit(): void {
    this.getStatus();
  }


  statusInfo: any;
  orderId = 1; 

  getStatus() {
    this.ds.getLatestStatus(this.orderId).subscribe({
      next: res => {
        console.log(res);
        this.statusInfo = res;
      }
    });
  }

 
  updateDeliveryStatus(status: string) {
    this.ds.updateStatus(this.orderId, status).subscribe({
      next: res => {
        alert(res);
        this.getStatus();
      }
    });
  }

 
  otpForm = new FormGroup({
    orderId: new FormControl(this.orderId),
    otp: new FormControl('')
  });

  verifyDeliveryOtp(data: any) {
    this.ds.verifyOtp(data).subscribe({
      next: res => {
        alert("Order Verified Successfully");
        this.getStatus();
      },
      error: err => {
        alert("Invalid OTP or expired");
      }
    });
  }

  isStage(stage: string): boolean {
  if (!this.statusInfo) return false;
  const order = ['Initiated', 'In Process', 'Out for Delivery', 'Delivered'];
  const currentIndex = order.indexOf(this.statusInfo.status);
  const checkIndex = order.indexOf(stage);
  return checkIndex <= currentIndex;
}

  }
