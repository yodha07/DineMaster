import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { DeliveryAddress } from "./delivery-address/delivery-address";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, DeliveryAddress],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App {
  protected title = 'DineMasterConsumingAngular';
}
