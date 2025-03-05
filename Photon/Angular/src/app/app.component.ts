import { Component } from '@angular/core';
import('@photonhealth/webcomponents').catch(() => {});
import { PhotonClient } from '@photonhealth/sdk';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'photon-app';

  public log() {
    console.log("treatment prescribed!");
  }
}

// import { Component } from '@angular/core';
// import { RouterOutlet } from '@angular/router';

// @Component({
  // selector: 'app-root',
  // standalone: true,
  // imports: [RouterOutlet],
  // templateUrl: './app.component.html',
  // styleUrl: './app.component.css'
// })
// export class AppComponent {
  // title = 'photon-app';
// }
