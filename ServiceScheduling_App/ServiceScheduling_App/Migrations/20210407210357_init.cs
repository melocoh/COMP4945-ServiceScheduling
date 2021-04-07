using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ServiceScheduling_App.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CertificationType",
                columns: table => new
                {
                    CertId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CertTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CertificationType", x => x.CertId);
                });

            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "JobType",
                columns: table => new
                {
                    JobId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobType", x => x.JobId);
                });

            migrationBuilder.CreateTable(
                name: "ServiceType",
                columns: table => new
                {
                    ServId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CertificationRqt = table.Column<int>(type: "int", nullable: false),
                    MaxNoEmp = table.Column<int>(type: "int", nullable: false),
                    MaxNoClient = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceType", x => x.ServId);
                    table.ForeignKey(
                        name: "FK_ServiceType_CertificationType_CertificationRqt",
                        column: x => x.CertificationRqt,
                        principalTable: "CertificationType",
                        principalColumn: "CertId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmpId);
                    table.ForeignKey(
                        name: "FK_Employee_JobType_JobId",
                        column: x => x.JobId,
                        principalTable: "JobType",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    AppId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServId = table.Column<int>(type: "int", nullable: false),
                    EntryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalFee = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.AppId);
                    table.ForeignKey(
                        name: "FK_Appointment_ServiceType_ServId",
                        column: x => x.ServId,
                        principalTable: "ServiceType",
                        principalColumn: "ServId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceShift",
                columns: table => new
                {
                    ServiceShiftId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServId = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    TimeStart = table.Column<TimeSpan>(type: "time", nullable: false),
                    TimeEnd = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceShift", x => x.ServiceShiftId);
                    table.ForeignKey(
                        name: "FK_ServiceShift_ServiceType_ServId",
                        column: x => x.ServId,
                        principalTable: "ServiceType",
                        principalColumn: "ServId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmpCertifications",
                columns: table => new
                {
                    EmpId = table.Column<int>(type: "int", nullable: false),
                    CertId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpCertifications", x => new { x.EmpId, x.CertId });
                    table.ForeignKey(
                        name: "FK_EmpCertifications_CertificationType_CertId",
                        column: x => x.CertId,
                        principalTable: "CertificationType",
                        principalColumn: "CertId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpCertifications_Employee_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employee",
                        principalColumn: "EmpId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentSession",
                columns: table => new
                {
                    AppSessionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppId = table.Column<int>(type: "int", nullable: false),
                    SessionNo = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentSession", x => x.AppSessionId);
                    table.ForeignKey(
                        name: "FK_AppointmentSession_Appointment_AppId",
                        column: x => x.AppId,
                        principalTable: "Appointment",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ClientAppointments",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    AppId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientAppointments", x => new { x.ClientId, x.AppId });
                    table.ForeignKey(
                        name: "FK_ClientAppointments_Appointment_AppId",
                        column: x => x.AppId,
                        principalTable: "Appointment",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientAppointments_Client_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Client",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmpAppointments",
                columns: table => new
                {
                    EmpId = table.Column<int>(type: "int", nullable: false),
                    AppId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpAppointments", x => new { x.EmpId, x.AppId });
                    table.ForeignKey(
                        name: "FK_EmpAppointments_Appointment_AppId",
                        column: x => x.AppId,
                        principalTable: "Appointment",
                        principalColumn: "AppId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpAppointments_Employee_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employee",
                        principalColumn: "EmpId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmpShifts",
                columns: table => new
                {
                    EmpId = table.Column<int>(type: "int", nullable: false),
                    ServiceShiftId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmpShifts", x => new { x.EmpId, x.ServiceShiftId });
                    table.ForeignKey(
                        name: "FK_EmpShifts_Employee_EmpId",
                        column: x => x.EmpId,
                        principalTable: "Employee",
                        principalColumn: "EmpId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmpShifts_ServiceShift_ServiceShiftId",
                        column: x => x.ServiceShiftId,
                        principalTable: "ServiceShift",
                        principalColumn: "ServiceShiftId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_ServId",
                table: "Appointment",
                column: "ServId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentSession_AppId",
                table: "AppointmentSession",
                column: "AppId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ClientAppointments_AppId",
                table: "ClientAppointments",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpAppointments_AppId",
                table: "EmpAppointments",
                column: "AppId");

            migrationBuilder.CreateIndex(
                name: "IX_EmpCertifications_CertId",
                table: "EmpCertifications",
                column: "CertId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_JobId",
                table: "Employee",
                column: "JobId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmpShifts_ServiceShiftId",
                table: "EmpShifts",
                column: "ServiceShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceShift_ServId",
                table: "ServiceShift",
                column: "ServId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceType_CertificationRqt",
                table: "ServiceType",
                column: "CertificationRqt",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentSession");

            migrationBuilder.DropTable(
                name: "ClientAppointments");

            migrationBuilder.DropTable(
                name: "EmpAppointments");

            migrationBuilder.DropTable(
                name: "EmpCertifications");

            migrationBuilder.DropTable(
                name: "EmpShifts");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "ServiceShift");

            migrationBuilder.DropTable(
                name: "JobType");

            migrationBuilder.DropTable(
                name: "ServiceType");

            migrationBuilder.DropTable(
                name: "CertificationType");
        }
    }
}
