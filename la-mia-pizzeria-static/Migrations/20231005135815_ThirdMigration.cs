using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace la_mia_pizzeria_static.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientPizza_Ingredient_IngredientsIngredientID",
                table: "IngredientPizza");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient");

            migrationBuilder.RenameTable(
                name: "Ingredient",
                newName: "Ingredients");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                column: "IngredientID");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientPizza_Ingredients_IngredientsIngredientID",
                table: "IngredientPizza",
                column: "IngredientsIngredientID",
                principalTable: "Ingredients",
                principalColumn: "IngredientID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientPizza_Ingredients_IngredientsIngredientID",
                table: "IngredientPizza");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.RenameTable(
                name: "Ingredients",
                newName: "Ingredient");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient",
                column: "IngredientID");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientPizza_Ingredient_IngredientsIngredientID",
                table: "IngredientPizza",
                column: "IngredientsIngredientID",
                principalTable: "Ingredient",
                principalColumn: "IngredientID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
