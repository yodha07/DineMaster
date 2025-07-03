import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { DeliveryService } from '../delivery-service';
declare var $: any;

@Component({
  selector: 'app-delivery-address',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './delivery-address.html',
  styleUrl: './delivery-address.scss'
})
export class DeliveryAddress implements OnInit {
  constructor(private es: DeliveryService) {}

  fetchaddress: any;
  id: any;

  address = new FormGroup({
    addressId: new FormControl(0),
    fullAddress: new FormControl(''),
    pincode: new FormControl(''),
    city: new FormControl(''),
    state: new FormControl(''),
    landmark: new FormControl(''),
    userId: new FormControl('1')
  });

  ngOnInit(): void {
    this.FetchAddress();
  }

  FetchAddress() {
    this.es.FetchDeliveryAddress().subscribe({
      next: res => {
        this.fetchaddress = res;
      },
      error: err => {
        console.log(err);
      }
    });
  }

  AddDeliveryAdd(data: any) {
    this.es.AddDeliveryAddress(data).subscribe({
      next: res => {
        alert('Address Added Successfully');
        $('#exampleModal').modal('hide');
        this.address.reset();
        this.FetchAddress();
      },
      error: err => {
        console.log(err);
      }
    });
  }

  deleteAddress(id: any) {
    if (confirm("Are you sure you want to delete this address?")) {
      this.es.deleteAddress(id).subscribe({
        next: res => {
          alert('Deleted Successfully');
          this.FetchAddress();
        }
      });
    }
  }


  EditDeliveryAddId(data: any) {
    this.id = data.addressId;
    this.address.controls.fullAddress.setValue(data.fullAddress);
    this.address.controls.pincode.setValue(data.pincode);
    this.address.controls.city.setValue(data.city);
    this.address.controls.state.setValue(data.state);
    this.address.controls.landmark.setValue(data.landmark);
    this.address.controls.userId.setValue(data.userId);
    this.address.controls.addressId.setValue(data.addressId);
  }

  EditDeliveryAdd() {
    this.es.updateDeliveryAddress(this.address.value, this.id).subscribe({
      next:res=>{
        alert('Address Updated Successfully');
        $('#exampleModal2').modal('hide');
        this.address.reset();
        this.FetchAddress();
      },
      // error:err => {
      //   console.log(err);
      // }
    });
  }
}
