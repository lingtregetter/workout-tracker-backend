using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.DAL.EF.Migrations
{
    /// <inheritdoc />
    public partial class Casc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppRefreshTokens_AspNetUsers_AppUserId",
                table: "AppRefreshTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseMuscles_Exercises_ExerciseId",
                table: "ExerciseMuscles");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseMuscles_MuscleGroups_MuscleGroupId",
                table: "ExerciseMuscles");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInformations_AspNetUsers_AppUserId",
                table: "PersonalInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reps_WorkoutSets_WorkoutSetId",
                table: "Reps");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingBlocks_AspNetUsers_AppUserId",
                table: "TrainingBlocks");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingBlocks_TrainingPrograms_TrainingProgramId",
                table: "TrainingBlocks");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPrograms_AspNetUsers_AppUserId",
                table: "TrainingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPrograms_AspNetUsers_AppUserId",
                table: "UserPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPrograms_TrainingPrograms_TrainingProgramId",
                table: "UserPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_Weights_WorkoutSets_WorkoutSetId",
                table: "Weights");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExercises_Exercises_ExerciseId",
                table: "WorkoutExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExercises_Workouts_WorkoutId",
                table: "WorkoutExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_AspNetUsers_AppUserId",
                table: "Workouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_TrainingBlocks_TrainingBlockId",
                table: "Workouts");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSets_WorkoutExercises_WorkoutExerciseId",
                table: "WorkoutSets");

            migrationBuilder.AddForeignKey(
                name: "FK_AppRefreshTokens_AspNetUsers_AppUserId",
                table: "AppRefreshTokens",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseMuscles_Exercises_ExerciseId",
                table: "ExerciseMuscles",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseMuscles_MuscleGroups_MuscleGroupId",
                table: "ExerciseMuscles",
                column: "MuscleGroupId",
                principalTable: "MuscleGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInformations_AspNetUsers_AppUserId",
                table: "PersonalInformations",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reps_WorkoutSets_WorkoutSetId",
                table: "Reps",
                column: "WorkoutSetId",
                principalTable: "WorkoutSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingBlocks_AspNetUsers_AppUserId",
                table: "TrainingBlocks",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingBlocks_TrainingPrograms_TrainingProgramId",
                table: "TrainingBlocks",
                column: "TrainingProgramId",
                principalTable: "TrainingPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPrograms_AspNetUsers_AppUserId",
                table: "TrainingPrograms",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPrograms_AspNetUsers_AppUserId",
                table: "UserPrograms",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPrograms_TrainingPrograms_TrainingProgramId",
                table: "UserPrograms",
                column: "TrainingProgramId",
                principalTable: "TrainingPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Weights_WorkoutSets_WorkoutSetId",
                table: "Weights",
                column: "WorkoutSetId",
                principalTable: "WorkoutSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExercises_Exercises_ExerciseId",
                table: "WorkoutExercises",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExercises_Workouts_WorkoutId",
                table: "WorkoutExercises",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_AspNetUsers_AppUserId",
                table: "Workouts",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_TrainingBlocks_TrainingBlockId",
                table: "Workouts",
                column: "TrainingBlockId",
                principalTable: "TrainingBlocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutSets_WorkoutExercises_WorkoutExerciseId",
                table: "WorkoutSets",
                column: "WorkoutExerciseId",
                principalTable: "WorkoutExercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppRefreshTokens_AspNetUsers_AppUserId",
                table: "AppRefreshTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseMuscles_Exercises_ExerciseId",
                table: "ExerciseMuscles");

            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseMuscles_MuscleGroups_MuscleGroupId",
                table: "ExerciseMuscles");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalInformations_AspNetUsers_AppUserId",
                table: "PersonalInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reps_WorkoutSets_WorkoutSetId",
                table: "Reps");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingBlocks_AspNetUsers_AppUserId",
                table: "TrainingBlocks");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingBlocks_TrainingPrograms_TrainingProgramId",
                table: "TrainingBlocks");

            migrationBuilder.DropForeignKey(
                name: "FK_TrainingPrograms_AspNetUsers_AppUserId",
                table: "TrainingPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPrograms_AspNetUsers_AppUserId",
                table: "UserPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPrograms_TrainingPrograms_TrainingProgramId",
                table: "UserPrograms");

            migrationBuilder.DropForeignKey(
                name: "FK_Weights_WorkoutSets_WorkoutSetId",
                table: "Weights");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExercises_Exercises_ExerciseId",
                table: "WorkoutExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutExercises_Workouts_WorkoutId",
                table: "WorkoutExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_AspNetUsers_AppUserId",
                table: "Workouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Workouts_TrainingBlocks_TrainingBlockId",
                table: "Workouts");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutSets_WorkoutExercises_WorkoutExerciseId",
                table: "WorkoutSets");

            migrationBuilder.AddForeignKey(
                name: "FK_AppRefreshTokens_AspNetUsers_AppUserId",
                table: "AppRefreshTokens",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseMuscles_Exercises_ExerciseId",
                table: "ExerciseMuscles",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseMuscles_MuscleGroups_MuscleGroupId",
                table: "ExerciseMuscles",
                column: "MuscleGroupId",
                principalTable: "MuscleGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalInformations_AspNetUsers_AppUserId",
                table: "PersonalInformations",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reps_WorkoutSets_WorkoutSetId",
                table: "Reps",
                column: "WorkoutSetId",
                principalTable: "WorkoutSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingBlocks_AspNetUsers_AppUserId",
                table: "TrainingBlocks",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingBlocks_TrainingPrograms_TrainingProgramId",
                table: "TrainingBlocks",
                column: "TrainingProgramId",
                principalTable: "TrainingPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TrainingPrograms_AspNetUsers_AppUserId",
                table: "TrainingPrograms",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPrograms_AspNetUsers_AppUserId",
                table: "UserPrograms",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserPrograms_TrainingPrograms_TrainingProgramId",
                table: "UserPrograms",
                column: "TrainingProgramId",
                principalTable: "TrainingPrograms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Weights_WorkoutSets_WorkoutSetId",
                table: "Weights",
                column: "WorkoutSetId",
                principalTable: "WorkoutSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExercises_Exercises_ExerciseId",
                table: "WorkoutExercises",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutExercises_Workouts_WorkoutId",
                table: "WorkoutExercises",
                column: "WorkoutId",
                principalTable: "Workouts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_AspNetUsers_AppUserId",
                table: "Workouts",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workouts_TrainingBlocks_TrainingBlockId",
                table: "Workouts",
                column: "TrainingBlockId",
                principalTable: "TrainingBlocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutSets_WorkoutExercises_WorkoutExerciseId",
                table: "WorkoutSets",
                column: "WorkoutExerciseId",
                principalTable: "WorkoutExercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
