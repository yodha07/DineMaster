import { Routes } from '@angular/router';
import { DeliveryAddress } from './delivery-address/delivery-address';
import { Home } from './home/home';
import {  DeliveryTrack} from './delivery-track/delivery-track';


export const routes: Routes = [

     {path:'' , redirectTo:'/home' , pathMatch:'full'},
     {path:'home' , component:Home},
    {path:'deliveryaddress' , component:DeliveryAddress},
    {path:'deliverytracking', component:DeliveryTrack }
];
