import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { List_Product } from 'src/app/contracts/list_product';
import {
  AlertifyService,
  MessageType,
  Position,
} from 'src/app/services/admin/alertify.service';
import { ProductService } from 'src/app/services/common/models/product.service';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss'],
})
export class ListComponent implements OnInit {
  constructor(
    private productService: ProductService,
    private alertifyService: AlertifyService
  ) {}

  displayedColumns: string[] = [
    'name',
    'stock',
    'price',
    'createDate',
    'updatedDate',
  ];
  dataSource: MatTableDataSource<List_Product> = null;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  async getProducts() {
    const item: { totalCount: number; products: List_Product[] } =
      await this.productService.read(
        !this.paginator ? 0 : this.paginator.pageIndex,
        !this.paginator ? 5 : this.paginator.pageSize,
        () =>
          this.alertifyService.message('ok', {
            dismissOthers: true,
            messageType: MessageType.Success,
            position: Position.TopRight,
          }),
        (errorMessage) =>
          this.alertifyService.message(errorMessage, {
            dismissOthers: true,
            messageType: MessageType.Error,
            position: Position.TopRight,
          })
      );
    this.dataSource = new MatTableDataSource<List_Product>(item.products);
    this.paginator.length = item.totalCount;
    this.dataSource.paginator = this.paginator;
  }

  async pageChanged() {
    await this.getProducts()
  }

  async ngOnInit() {
    await this.getProducts();
  }
}
