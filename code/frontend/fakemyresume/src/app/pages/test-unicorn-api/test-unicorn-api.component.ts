import { Component } from '@angular/core';
import { UnicornRewardsApiService } from '../../services/unicorn-rewards-api/unicorn-rewards-api.service';

@Component({
  selector: 'app-test-unicorn-api',
  standalone: true,
  imports: [],
  templateUrl: './test-unicorn-api.component.html',
  styleUrl: './test-unicorn-api.component.css'
})
export class TestUnicornApiComponent {

  constructor(private unicornApiService: UnicornRewardsApiService) { }

  testAuth(): boolean {
    console.log('clicked');
    this.unicornApiService.test("Hola Mundo").subscribe(() =>
      console.log('returned'));
    return true;
  }
}
