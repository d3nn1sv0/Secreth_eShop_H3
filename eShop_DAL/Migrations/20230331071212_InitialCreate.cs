using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eShop_DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "Baskets",
                columns: table => new
                {
                    BasketId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Baskets", x => x.BasketId);
                    table.ForeignKey(
                        name: "FK_Baskets_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "CustomerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BasketItems",
                columns: table => new
                {
                    BasketItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    BasketId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItems", x => x.BasketItemId);
                    table.ForeignKey(
                        name: "FK_BasketItems_Baskets_BasketId",
                        column: x => x.BasketId,
                        principalTable: "Baskets",
                        principalColumn: "BasketId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    ImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_Images_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Games" },
                    { 2, "Computers" },
                    { 3, "Electronics" },
                    { 4, "Music" },
                    { 5, "Grocery" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Email", "FirstName", "LastName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Carlotta44@gmail.com", "Carlotta", "Franecki", "(687) 839-1127 x3666" },
                    { 2, "Nicklaus.Nienow@hotmail.com", "Nicklaus", "Nienow", "303-973-3764 x32044" },
                    { 3, "Davin_Kuhlman@gmail.com", "Davin", "Kuhlman", "206.909.9554 x86645" },
                    { 4, "Ollie_Leffler38@hotmail.com", "Ollie", "Leffler", "(715) 511-5177 x681" },
                    { 5, "Alta.Beier43@hotmail.com", "Alta", "Beier", "255.940.8490" },
                    { 6, "Lora.Gaylord@yahoo.com", "Lora", "Gaylord", "687-503-7007 x3478" },
                    { 7, "Clifford40@gmail.com", "Clifford", "Mayer", "1-811-620-1613 x2584" },
                    { 8, "Broderick.Franecki@yahoo.com", "Broderick", "Franecki", "1-457-674-7180 x1314" },
                    { 9, "Nora.Zboncak2@yahoo.com", "Nora", "Zboncak", "264.876.2790" },
                    { 10, "Jeffrey.Borer@hotmail.com", "Jeffrey", "Borer", "582.507.1294 x36198" }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "SupplierId", "Email", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, null, "Franecki Inc", null },
                    { 2, null, "Kiehn LLC", null },
                    { 3, null, "Smitham - Simonis", null }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CustomerId", "OrderDate" },
                values: new object[,]
                {
                    { 1, 3, new DateTime(2023, 2, 1, 21, 14, 22, 493, DateTimeKind.Local).AddTicks(5527) },
                    { 2, 2, new DateTime(2023, 1, 9, 15, 8, 24, 567, DateTimeKind.Local).AddTicks(2927) },
                    { 3, 7, new DateTime(2022, 10, 21, 5, 38, 36, 350, DateTimeKind.Local).AddTicks(1828) },
                    { 4, 9, new DateTime(2022, 9, 20, 3, 43, 9, 171, DateTimeKind.Local).AddTicks(9822) },
                    { 5, 8, new DateTime(2022, 6, 1, 14, 45, 43, 827, DateTimeKind.Local).AddTicks(7039) },
                    { 6, 10, new DateTime(2022, 11, 21, 8, 34, 18, 367, DateTimeKind.Local).AddTicks(1594) },
                    { 7, 2, new DateTime(2023, 2, 4, 9, 0, 25, 632, DateTimeKind.Local).AddTicks(6491) },
                    { 8, 8, new DateTime(2023, 1, 6, 19, 0, 30, 655, DateTimeKind.Local).AddTicks(5588) },
                    { 9, 7, new DateTime(2022, 12, 5, 3, 7, 15, 589, DateTimeKind.Local).AddTicks(3385) },
                    { 10, 7, new DateTime(2022, 8, 21, 1, 44, 3, 100, DateTimeKind.Local).AddTicks(1016) },
                    { 11, 7, new DateTime(2022, 6, 29, 4, 30, 4, 478, DateTimeKind.Local).AddTicks(210) },
                    { 12, 4, new DateTime(2022, 6, 27, 15, 45, 8, 107, DateTimeKind.Local).AddTicks(6324) },
                    { 13, 9, new DateTime(2022, 10, 4, 10, 39, 14, 569, DateTimeKind.Local).AddTicks(9592) },
                    { 14, 10, new DateTime(2023, 1, 13, 7, 35, 4, 683, DateTimeKind.Local).AddTicks(3197) },
                    { 15, 4, new DateTime(2023, 3, 7, 19, 18, 22, 43, DateTimeKind.Local).AddTicks(8377) },
                    { 16, 4, new DateTime(2022, 6, 17, 6, 54, 25, 263, DateTimeKind.Local).AddTicks(4392) },
                    { 17, 8, new DateTime(2022, 11, 7, 4, 47, 24, 444, DateTimeKind.Local).AddTicks(3414) },
                    { 18, 5, new DateTime(2022, 8, 19, 17, 22, 19, 241, DateTimeKind.Local).AddTicks(7314) },
                    { 19, 3, new DateTime(2022, 11, 11, 0, 17, 35, 752, DateTimeKind.Local).AddTicks(5014) },
                    { 20, 5, new DateTime(2023, 3, 12, 7, 18, 47, 378, DateTimeKind.Local).AddTicks(7866) }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Description", "Name", "Price", "SupplierId" },
                values: new object[,]
                {
                    { 1, 4, "Odio nam non quo.", "Rustic Concrete Bike", 83.12073893012509300m, 2 },
                    { 2, 4, "Repellendus voluptatem rerum est.", "Tasty Wooden Computer", 62.14230838052108200m, 2 },
                    { 3, 2, "Non doloribus et corporis repellendus iure qui assumenda laborum.", "Handmade Cotton Shoes", 49.31449137363326500m, 1 },
                    { 4, 1, "Corrupti perferendis animi nulla.", "Small Granite Table", 96.30990772056859600m, 3 },
                    { 5, 5, "A officia occaecati corrupti dolorem est id qui sed est.", "Small Rubber Car", 53.03149088240758600m, 3 },
                    { 6, 4, "Ea odit officia qui sit sint voluptatem maxime.", "Generic Cotton Shoes", 79.9037150339706400m, 3 },
                    { 7, 1, "Quisquam reprehenderit iusto sit explicabo repellat natus error.", "Rustic Steel Car", 45.39610054362379600m, 3 },
                    { 8, 1, "Aliquam culpa natus quia id non voluptatum at.", "Practical Frozen Computer", 77.72979309211008100m, 2 },
                    { 9, 4, "Corporis quos quo vero velit in voluptatem velit.", "Handmade Steel Chair", 41.32004616098479900m, 3 },
                    { 10, 3, "Ab amet et neque iure.", "Awesome Wooden Keyboard", 30.0569480322566500m, 3 },
                    { 11, 3, "Distinctio aut quisquam aliquam provident natus quisquam.", "Practical Wooden Bike", 71.3466254637328600m, 3 },
                    { 12, 5, "Eum sequi laudantium.", "Ergonomic Fresh Car", 76.49448791634966700m, 2 },
                    { 13, 4, "Sunt eos et iusto.", "Handmade Granite Chair", 71.53853069177754400m, 1 },
                    { 14, 5, "Sunt nobis corrupti nemo reiciendis quam.", "Handmade Frozen Chair", 42.38993594441094400m, 1 },
                    { 15, 5, "Voluptates iusto quis eligendi ratione.", "Ergonomic Soft Computer", 91.27560081532019500m, 1 },
                    { 16, 2, "Rerum veniam repellat facere quos iure rerum sit.", "Ergonomic Granite Keyboard", 61.88873156061800500m, 2 },
                    { 17, 3, "Nemo et quisquam aspernatur suscipit possimus sit cupiditate ad veniam.", "Tasty Granite Chair", 56.95427185993374400m, 1 },
                    { 18, 1, "Quia quidem doloremque eos.", "Intelligent Plastic Chips", 42.58184117245572700m, 3 },
                    { 19, 3, "Et ut qui labore dolores repellat quas iste.", "Handcrafted Soft Computer", 37.52107937565125500m, 3 },
                    { 20, 5, "Totam non qui voluptates autem exercitationem exercitationem voluptas.", "Unbranded Rubber Cheese", 65.84525356248270100m, 3 },
                    { 21, 4, "Ea non libero nam et nemo rerum voluptas iusto eaque.", "Licensed Fresh Shoes", 72.42808208494825900m, 2 },
                    { 22, 2, "Autem optio et debitis officiis ut.", "Handcrafted Granite Fish", 30.00942633348020800m, 1 },
                    { 23, 2, "Modi et veritatis blanditiis.", "Generic Soft Table", 4.836622430866871800m, 1 },
                    { 24, 1, "Animi eum accusamus architecto provident nam fuga.", "Tasty Fresh Shirt", 16.85463078918614500m, 2 },
                    { 25, 3, "Non et autem.", "Fantastic Concrete Pizza", 29.82120748321582900m, 2 },
                    { 26, 2, "Odit cumque quia distinctio adipisci voluptatum totam cupiditate veniam.", "Rustic Frozen Ball", 8.085315227548274200m, 2 },
                    { 27, 4, "Sit culpa voluptatem qui qui molestiae modi quam.", "Small Concrete Pizza", 51.47768444590164700m, 3 },
                    { 28, 5, "Eligendi aspernatur ab delectus fugiat corrupti.", "Handcrafted Concrete Cheese", 96.39829605463814800m, 2 },
                    { 29, 5, "Possimus eaque mollitia sequi qui et facilis similique officia.", "Ergonomic Fresh Salad", 70.41502348306352200m, 3 },
                    { 30, 2, "Id consequatur magnam non quidem officia qui.", "Sleek Soft Keyboard", 98.2730492894878300m, 1 },
                    { 31, 2, "Corrupti rerum sint praesentium distinctio rem beatae porro.", "Rustic Steel Soap", 21.49096946208320800m, 1 },
                    { 32, 3, "Quis ex minus.", "Refined Wooden Shirt", 81.37885129935057100m, 1 },
                    { 33, 4, "Ipsum facilis laborum ut.", "Gorgeous Rubber Shirt", 44.93119650703448500m, 2 },
                    { 34, 3, "Neque sit explicabo.", "Tasty Fresh Bike", 68.22280048542790600m, 3 },
                    { 35, 2, "Atque repellat non delectus et odio et ipsum tempore.", "Generic Fresh Table", 20.96592653587737700m, 1 },
                    { 36, 1, "Vel voluptas animi sint repudiandae quia.", "Incredible Granite Bacon", 19.15601185669932100m, 1 },
                    { 37, 2, "Dolores nobis qui.", "Incredible Metal Keyboard", 95.70534987827079100m, 3 },
                    { 38, 5, "Enim aliquid repellendus eius aut minus accusantium est id.", "Generic Metal Sausages", 85.08386606959810900m, 1 },
                    { 39, 3, "Minima vel molestiae quo ipsam et aut dolorem ut.", "Refined Plastic Salad", 52.3057271201655400m, 1 },
                    { 40, 1, "Officiis ut nostrum neque magni maiores ipsum ut aut.", "Tasty Soft Mouse", 65.1321977168006200m, 1 },
                    { 41, 3, "Quae quas ea eum possimus aut.", "Small Steel Pizza", 48.20773901101558300m, 1 },
                    { 42, 2, "Nulla quisquam autem nam.", "Generic Metal Shoes", 19.94737003787762200m, 3 },
                    { 43, 1, "Rerum distinctio minus libero assumenda.", "Small Soft Fish", 82.75272973196239900m, 2 },
                    { 44, 3, "Ea ex accusantium est dolores repellendus.", "Handcrafted Wooden Keyboard", 92.47568795386506100m, 1 },
                    { 45, 2, "Labore dolorum quo vero aut nihil corporis est fugit amet.", "Handcrafted Wooden Ball", 10.57228907829723100m, 3 },
                    { 46, 2, "Illum doloribus quo aliquam.", "Handmade Cotton Shoes", 39.726063120982600m, 1 },
                    { 47, 4, "Numquam voluptatibus sapiente ut voluptatum quod non quia corporis.", "Practical Cotton Bike", 62.95807823443697800m, 3 },
                    { 48, 1, "Non voluptate nobis quae.", "Refined Metal Ball", 51.43203216718143400m, 1 },
                    { 49, 2, "Pariatur nemo tempore et animi velit deleniti iste.", "Refined Wooden Towels", 13.84123298052756700m, 2 },
                    { 50, 3, "Ipsum similique sed quaerat.", "Unbranded Steel Bike", 88.16545771815138400m, 1 }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "ImageId", "ProductId", "Url" },
                values: new object[,]
                {
                    { 1, 13, "https://picsum.photos/640/480/?image=170" },
                    { 2, 7, "https://picsum.photos/640/480/?image=240" },
                    { 3, 34, "https://picsum.photos/640/480/?image=479" },
                    { 4, 42, "https://picsum.photos/640/480/?image=571" },
                    { 5, 36, "https://picsum.photos/640/480/?image=900" },
                    { 6, 50, "https://picsum.photos/640/480/?image=386" },
                    { 7, 6, "https://picsum.photos/640/480/?image=163" },
                    { 8, 40, "https://picsum.photos/640/480/?image=248" },
                    { 9, 33, "https://picsum.photos/640/480/?image=345" },
                    { 10, 31, "https://picsum.photos/640/480/?image=660" },
                    { 11, 32, "https://picsum.photos/640/480/?image=818" },
                    { 12, 20, "https://picsum.photos/640/480/?image=822" },
                    { 13, 42, "https://picsum.photos/640/480/?image=528" },
                    { 14, 50, "https://picsum.photos/640/480/?image=229" },
                    { 15, 16, "https://picsum.photos/640/480/?image=70" },
                    { 16, 20, "https://picsum.photos/640/480/?image=853" },
                    { 17, 39, "https://picsum.photos/640/480/?image=428" },
                    { 18, 25, "https://picsum.photos/640/480/?image=664" },
                    { 19, 11, "https://picsum.photos/640/480/?image=417" },
                    { 20, 24, "https://picsum.photos/640/480/?image=56" },
                    { 21, 12, "https://picsum.photos/640/480/?image=471" },
                    { 22, 1, "https://picsum.photos/640/480/?image=528" },
                    { 23, 43, "https://picsum.photos/640/480/?image=651" },
                    { 24, 4, "https://picsum.photos/640/480/?image=1044" },
                    { 25, 1, "https://picsum.photos/640/480/?image=1028" },
                    { 26, 4, "https://picsum.photos/640/480/?image=688" },
                    { 27, 48, "https://picsum.photos/640/480/?image=1081" },
                    { 28, 27, "https://picsum.photos/640/480/?image=637" },
                    { 29, 42, "https://picsum.photos/640/480/?image=530" },
                    { 30, 31, "https://picsum.photos/640/480/?image=702" },
                    { 31, 27, "https://picsum.photos/640/480/?image=492" },
                    { 32, 27, "https://picsum.photos/640/480/?image=838" },
                    { 33, 49, "https://picsum.photos/640/480/?image=996" },
                    { 34, 20, "https://picsum.photos/640/480/?image=638" },
                    { 35, 38, "https://picsum.photos/640/480/?image=542" },
                    { 36, 6, "https://picsum.photos/640/480/?image=435" },
                    { 37, 8, "https://picsum.photos/640/480/?image=639" },
                    { 38, 26, "https://picsum.photos/640/480/?image=198" },
                    { 39, 38, "https://picsum.photos/640/480/?image=162" },
                    { 40, 33, "https://picsum.photos/640/480/?image=864" },
                    { 41, 8, "https://picsum.photos/640/480/?image=949" },
                    { 42, 3, "https://picsum.photos/640/480/?image=47" },
                    { 43, 37, "https://picsum.photos/640/480/?image=726" },
                    { 44, 22, "https://picsum.photos/640/480/?image=425" },
                    { 45, 5, "https://picsum.photos/640/480/?image=197" },
                    { 46, 29, "https://picsum.photos/640/480/?image=1083" },
                    { 47, 23, "https://picsum.photos/640/480/?image=614" },
                    { 48, 45, "https://picsum.photos/640/480/?image=14" },
                    { 49, 49, "https://picsum.photos/640/480/?image=504" },
                    { 50, 32, "https://picsum.photos/640/480/?image=101" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_BasketId",
                table: "BasketItems",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_ProductId",
                table: "BasketItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_CustomerId",
                table: "Baskets",
                column: "CustomerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductId",
                table: "Images",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierId",
                table: "Products",
                column: "SupplierId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketItems");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Baskets");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
