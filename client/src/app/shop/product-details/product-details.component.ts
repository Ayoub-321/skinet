import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {

  product: IProduct;

  constructor(private shopService: ShopService, private activatedRoute: ActivatedRoute) { }

  ngOnInit(): void
  {
    this.loadProduct();
  }

  loadProduct()
  {
    const param =this.activatedRoute.snapshot.paramMap.get('id')
    const id = param?+param:0;
    this.shopService
    .getProduct(id)
    .subscribe(product => {
      this.product = product;
    }, error =>{
      console.log(error);
    });
  }

}
