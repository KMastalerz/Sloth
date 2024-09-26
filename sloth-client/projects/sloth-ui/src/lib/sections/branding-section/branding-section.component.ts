import { Component, computed, OnInit, signal } from '@angular/core';
import { BaseSection } from '../../engine/base/base-section/base-section.component';

@Component({
  selector: 'sl-branding-section',
  standalone: true,
  imports: [],
  templateUrl: './branding-section.component.html',
  styleUrl: './branding-section.component.scss'
})
export class BrandingSectionComponent extends BaseSection implements OnInit {
  title = signal<string>('Sloth');
  imgPath = signal<string>('assets/icon.png');
  imgAlt = computed(() => `${this.title} logo`);

  ngOnInit(): void {

  }
}
