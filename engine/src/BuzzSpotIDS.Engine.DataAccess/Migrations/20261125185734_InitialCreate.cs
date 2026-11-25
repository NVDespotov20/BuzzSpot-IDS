using System;
using System.Net;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BuzzSpotIDS.Engine.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "engine");

            migrationBuilder.CreateTable(
                name: "monitored_sources",
                schema: "engine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    BaseUrl = table.Column<string>(type: "character varying(2048)", maxLength: 2048, nullable: true),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_monitored_sources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "rules",
                schema: "engine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rules", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "events",
                schema: "engine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SourceId = table.Column<Guid>(type: "uuid", nullable: false),
                    OccurredAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    ReceivedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    TraceId = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: true),
                    SpanId = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_events_monitored_sources_SourceId",
                        column: x => x.SourceId,
                        principalSchema: "engine",
                        principalTable: "monitored_sources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "rule_versions",
                schema: "engine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RuleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Severity = table.Column<short>(type: "smallint", nullable: false),
                    DefinitionJson = table.Column<string>(type: "jsonb", nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rule_versions", x => x.Id);
                    table.CheckConstraint("ck_rule_severity", "\"Severity\" BETWEEN 1 AND 4");
                    table.CheckConstraint("ck_rule_version_positive", "\"Version\" > 0");
                    table.ForeignKey(
                        name: "FK_rule_versions_rules_RuleId",
                        column: x => x.RuleId,
                        principalSchema: "engine",
                        principalTable: "rules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "application_logs",
                schema: "engine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Level = table.Column<short>(type: "smallint", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: true),
                    Exception = table.Column<string>(type: "text", nullable: true),
                    PropertiesJson = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_application_logs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_application_logs_events_Id",
                        column: x => x.Id,
                        principalSchema: "engine",
                        principalTable: "events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "http_events",
                schema: "engine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Method = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    Scheme = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false),
                    Host = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Path = table.Column<string>(type: "text", nullable: false),
                    QueryString = table.Column<string>(type: "text", nullable: true),
                    ClientIp = table.Column<IPAddress>(type: "inet", nullable: true),
                    StatusCode = table.Column<int>(type: "integer", nullable: true),
                    DurationMs = table.Column<long>(type: "bigint", nullable: true),
                    RequestSizeBytes = table.Column<long>(type: "bigint", nullable: true),
                    ResponseSizeBytes = table.Column<long>(type: "bigint", nullable: true),
                    RequestHeadersJson = table.Column<string>(type: "jsonb", nullable: true),
                    ResponseHeadersJson = table.Column<string>(type: "jsonb", nullable: true),
                    RequestBodySample = table.Column<string>(type: "text", nullable: true),
                    ResponseBodySample = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_http_events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_http_events_events_Id",
                        column: x => x.Id,
                        principalSchema: "engine",
                        principalTable: "events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "queued_events",
                schema: "engine",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    EnqueuedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    AvailableAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    AttemptCount = table.Column<int>(type: "integer", nullable: false),
                    LockedBy = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    LeaseToken = table.Column<Guid>(type: "uuid", nullable: true),
                    LockedUntil = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    DeadLetteredAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LastError = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_queued_events", x => x.Id);
                    table.CheckConstraint("ck_queue_attempt_count", "\"AttemptCount\" >= 0");
                    table.CheckConstraint("ck_queue_lease", "(\n    \"LockedBy\" IS NULL\n    AND \"LockedUntil\" IS NULL\n    AND \"LeaseToken\" IS NULL\n)\nOR\n(\n    \"LockedBy\" IS NOT NULL\n    AND \"LockedUntil\" IS NOT NULL\n    AND \"LeaseToken\" IS NOT NULL\n)");
                    table.ForeignKey(
                        name: "FK_queued_events_events_EventId",
                        column: x => x.EventId,
                        principalSchema: "engine",
                        principalTable: "events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "alerts",
                schema: "engine",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    RuleVersionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<short>(type: "smallint", nullable: false),
                    EvidenceJson = table.Column<string>(type: "jsonb", nullable: true),
                    DetectedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alerts", x => x.Id);
                    table.CheckConstraint("ck_alert_status", "\"Status\" BETWEEN 1 AND 4");
                    table.ForeignKey(
                        name: "FK_alerts_events_EventId",
                        column: x => x.EventId,
                        principalSchema: "engine",
                        principalTable: "events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_alerts_rule_versions_RuleVersionId",
                        column: x => x.RuleVersionId,
                        principalSchema: "engine",
                        principalTable: "rule_versions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_alerts_EventId_RuleVersionId",
                schema: "engine",
                table: "alerts",
                columns: new[] { "EventId", "RuleVersionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_alerts_RuleVersionId",
                schema: "engine",
                table: "alerts",
                column: "RuleVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_alerts_Status_DetectedAt",
                schema: "engine",
                table: "alerts",
                columns: new[] { "Status", "DetectedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_events_OccurredAt",
                schema: "engine",
                table: "events",
                column: "OccurredAt");

            migrationBuilder.CreateIndex(
                name: "IX_events_SourceId_OccurredAt",
                schema: "engine",
                table: "events",
                columns: new[] { "SourceId", "OccurredAt" });

            migrationBuilder.CreateIndex(
                name: "IX_monitored_sources_Name",
                schema: "engine",
                table: "monitored_sources",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_queued_events_AvailableAt_EnqueuedAt",
                schema: "engine",
                table: "queued_events",
                columns: new[] { "AvailableAt", "EnqueuedAt" },
                filter: "\"DeadLetteredAt\" IS NULL");

            migrationBuilder.CreateIndex(
                name: "IX_queued_events_EventId",
                schema: "engine",
                table: "queued_events",
                column: "EventId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_rule_versions_RuleId_Version",
                schema: "engine",
                table: "rule_versions",
                columns: new[] { "RuleId", "Version" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_rules_Code",
                schema: "engine",
                table: "rules",
                column: "Code",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "alerts",
                schema: "engine");

            migrationBuilder.DropTable(
                name: "application_logs",
                schema: "engine");

            migrationBuilder.DropTable(
                name: "http_events",
                schema: "engine");

            migrationBuilder.DropTable(
                name: "queued_events",
                schema: "engine");

            migrationBuilder.DropTable(
                name: "rule_versions",
                schema: "engine");

            migrationBuilder.DropTable(
                name: "events",
                schema: "engine");

            migrationBuilder.DropTable(
                name: "rules",
                schema: "engine");

            migrationBuilder.DropTable(
                name: "monitored_sources",
                schema: "engine");
        }
    }
}
