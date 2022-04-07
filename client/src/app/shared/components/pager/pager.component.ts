import { Component, Input, OnInit, Output, EventEmitter} from '@angular/core';
// import { EventEmitter } from 'stream';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss']
})
export class PagerComponent implements OnInit {

  @Input() totalCount: number;
  @Input() pageSize: number;
  @Output() pageChanged = new EventEmitter<number>();

  constructor() { }

  ngOnInit(): void {
  }

  onPagerChenge(event: any)
  {
    this.pageChanged.emit(event.page);
  }

}
