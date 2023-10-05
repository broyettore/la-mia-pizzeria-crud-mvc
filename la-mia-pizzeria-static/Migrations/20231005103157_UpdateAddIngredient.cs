using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_static.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAddIngredient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ingredient",
                columns: table => new
                {
                    IngredientID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredient", x => x.IngredientID);
                });

            migrationBuilder.CreateTable(
                name: "IngredientPizza",
                columns: table => new
                {
                    IngredientsIngredientID = table.Column<int>(type: "int", nullable: false),
                    PizzasPizzaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientPizza", x => new { x.IngredientsIngredientID, x.PizzasPizzaId });
                    table.ForeignKey(
                        name: "FK_IngredientPizza_Ingredient_IngredientsIngredientID",
                        column: x => x.IngredientsIngredientID,
                        principalTable: "Ingredient",
                        principalColumn: "IngredientID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientPizza_Pizzas_PizzasPizzaId",
                        column: x => x.PizzasPizzaId,
                        principalTable: "Pizzas",
                        principalColumn: "PizzaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientPizza_PizzasPizzaId",
                table: "IngredientPizza",
                column: "PizzasPizzaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientPizza");

            migrationBuilder.DropTable(
                name: "Ingredient");
        }
    }
}
