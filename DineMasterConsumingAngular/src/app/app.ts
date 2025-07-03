import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { DeliveryAddress } from "./delivery-address/delivery-address";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink],
  templateUrl: './app.html',
  styleUrls: ['./app.scss']
})
export class App {
  protected title = 'DineMasterConsumingAngular';
}
