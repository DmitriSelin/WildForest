﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WildForest.Infrastructure.Persistence.Context;

#nullable disable

namespace WildForest.Infrastructure.Migrations
{
    [DbContext(typeof(WildForestDbContext))]
    [Migration("20230411210340_CountOfMarksMigration")]
    partial class CountOfMarksMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WildForest.Domain.Cities.Entities.City", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CountryId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("WildForest.Domain.Countries.Entities.Country", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("WildForest.Domain.Marks.Entities.Mark", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WeatherId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("WeatherId");

                    b.ToTable("Marks");
                });

            modelBuilder.Entity("WildForest.Domain.Tokens.Entities.RefreshToken", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("WildForest.Domain.Users.Entities.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CityId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer")
                        .HasColumnName("Role");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WildForest.Domain.Weather.Entities.WeatherForecast", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("CityId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("WeatherForecasts");
                });

            modelBuilder.Entity("WildForest.Domain.WeatherMarks.Entities.WeatherMark", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("WeatherId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("WeatherId")
                        .IsUnique();

                    b.ToTable("WeatherMarks");
                });

            modelBuilder.Entity("WildForest.Domain.Cities.Entities.City", b =>
                {
                    b.HasOne("WildForest.Domain.Countries.Entities.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("WildForest.Domain.Cities.ValueObjects.CityName", "CityName", b1 =>
                        {
                            b1.Property<string>("CityId")
                                .HasColumnType("text");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("CityName");

                            b1.HasKey("CityId");

                            b1.ToTable("Cities");

                            b1.WithOwner()
                                .HasForeignKey("CityId");
                        });

                    b.OwnsOne("WildForest.Domain.Cities.ValueObjects.Location", "Location", b1 =>
                        {
                            b1.Property<string>("CityId")
                                .HasColumnType("text");

                            b1.Property<double>("Latitude")
                                .HasColumnType("double precision")
                                .HasColumnName("Latitude");

                            b1.Property<double>("Longitude")
                                .HasColumnType("double precision")
                                .HasColumnName("Longitude");

                            b1.HasKey("CityId");

                            b1.ToTable("Cities");

                            b1.WithOwner()
                                .HasForeignKey("CityId");
                        });

                    b.Navigation("CityName")
                        .IsRequired();

                    b.Navigation("Country");

                    b.Navigation("Location")
                        .IsRequired();
                });

            modelBuilder.Entity("WildForest.Domain.Countries.Entities.Country", b =>
                {
                    b.OwnsOne("WildForest.Domain.Countries.ValueObjects.CountryName", "CountryName", b1 =>
                        {
                            b1.Property<string>("CountryId")
                                .HasColumnType("text");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("CountryName");

                            b1.HasKey("CountryId");

                            b1.ToTable("Countries");

                            b1.WithOwner()
                                .HasForeignKey("CountryId");
                        });

                    b.Navigation("CountryName")
                        .IsRequired();
                });

            modelBuilder.Entity("WildForest.Domain.Marks.Entities.Mark", b =>
                {
                    b.HasOne("WildForest.Domain.Users.Entities.User", "User")
                        .WithMany("Marks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WildForest.Domain.Weather.Entities.WeatherForecast", "WeatherForecast")
                        .WithMany("Marks")
                        .HasForeignKey("WeatherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("WildForest.Domain.Marks.ValueObjects.Comment", "Comment", b1 =>
                        {
                            b1.Property<string>("MarkId")
                                .HasColumnType("text");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(200)
                                .HasColumnType("character varying(200)")
                                .HasColumnName("Comment");

                            b1.HasKey("MarkId");

                            b1.ToTable("Marks");

                            b1.WithOwner()
                                .HasForeignKey("MarkId");
                        });

                    b.OwnsOne("WildForest.Domain.Marks.ValueObjects.Date", "Date", b1 =>
                        {
                            b1.Property<string>("MarkId")
                                .HasColumnType("text");

                            b1.Property<DateTime>("Value")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("Date");

                            b1.HasKey("MarkId");

                            b1.ToTable("Marks");

                            b1.WithOwner()
                                .HasForeignKey("MarkId");
                        });

                    b.OwnsOne("WildForest.Domain.Marks.ValueObjects.Rating", "Rating", b1 =>
                        {
                            b1.Property<string>("MarkId")
                                .HasColumnType("text");

                            b1.Property<byte>("Value")
                                .HasColumnType("smallint")
                                .HasColumnName("Rating");

                            b1.HasKey("MarkId");

                            b1.ToTable("Marks");

                            b1.WithOwner()
                                .HasForeignKey("MarkId");
                        });

                    b.Navigation("Comment");

                    b.Navigation("Date")
                        .IsRequired();

                    b.Navigation("Rating")
                        .IsRequired();

                    b.Navigation("User");

                    b.Navigation("WeatherForecast");
                });

            modelBuilder.Entity("WildForest.Domain.Tokens.Entities.RefreshToken", b =>
                {
                    b.HasOne("WildForest.Domain.Users.Entities.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("WildForest.Domain.Tokens.ValueObjects.CreatedByIp", "CreatedByIp", b1 =>
                        {
                            b1.Property<string>("RefreshTokenId")
                                .HasColumnType("text");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("CreatedByIp");

                            b1.HasKey("RefreshTokenId");

                            b1.ToTable("RefreshTokens");

                            b1.WithOwner()
                                .HasForeignKey("RefreshTokenId");
                        });

                    b.OwnsOne("WildForest.Domain.Tokens.ValueObjects.CreationDate", "CreationDate", b1 =>
                        {
                            b1.Property<string>("RefreshTokenId")
                                .HasColumnType("text");

                            b1.Property<DateTime>("Value")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("CreationDate");

                            b1.HasKey("RefreshTokenId");

                            b1.ToTable("RefreshTokens");

                            b1.WithOwner()
                                .HasForeignKey("RefreshTokenId");
                        });

                    b.OwnsOne("WildForest.Domain.Tokens.ValueObjects.Expiration", "Expiration", b1 =>
                        {
                            b1.Property<string>("RefreshTokenId")
                                .HasColumnType("text");

                            b1.Property<DateTime>("Value")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("Expiration");

                            b1.HasKey("RefreshTokenId");

                            b1.ToTable("RefreshTokens");

                            b1.WithOwner()
                                .HasForeignKey("RefreshTokenId");
                        });

                    b.OwnsOne("WildForest.Domain.Tokens.ValueObjects.ReasonRevoked", "ReasonRevoked", b1 =>
                        {
                            b1.Property<string>("RefreshTokenId")
                                .HasColumnType("text");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("ReasonRevoked");

                            b1.HasKey("RefreshTokenId");

                            b1.ToTable("RefreshTokens");

                            b1.WithOwner()
                                .HasForeignKey("RefreshTokenId");
                        });

                    b.OwnsOne("WildForest.Domain.Tokens.ValueObjects.ReplacedByToken", "ReplacedByToken", b1 =>
                        {
                            b1.Property<string>("RefreshTokenId")
                                .HasColumnType("text");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("ReplacedByToken");

                            b1.HasKey("RefreshTokenId");

                            b1.ToTable("RefreshTokens");

                            b1.WithOwner()
                                .HasForeignKey("RefreshTokenId");
                        });

                    b.OwnsOne("WildForest.Domain.Tokens.ValueObjects.Revoked", "Revoked", b1 =>
                        {
                            b1.Property<string>("RefreshTokenId")
                                .HasColumnType("text");

                            b1.Property<DateTime>("Value")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("Revoked");

                            b1.HasKey("RefreshTokenId");

                            b1.ToTable("RefreshTokens");

                            b1.WithOwner()
                                .HasForeignKey("RefreshTokenId");
                        });

                    b.OwnsOne("WildForest.Domain.Tokens.ValueObjects.RevokedByIp", "RevokedByIp", b1 =>
                        {
                            b1.Property<string>("RefreshTokenId")
                                .HasColumnType("text");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("RevokedByIp");

                            b1.HasKey("RefreshTokenId");

                            b1.ToTable("RefreshTokens");

                            b1.WithOwner()
                                .HasForeignKey("RefreshTokenId");
                        });

                    b.OwnsOne("WildForest.Domain.Tokens.ValueObjects.Token", "Token", b1 =>
                        {
                            b1.Property<string>("RefreshTokenId")
                                .HasColumnType("text");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Token");

                            b1.HasKey("RefreshTokenId");

                            b1.ToTable("RefreshTokens");

                            b1.WithOwner()
                                .HasForeignKey("RefreshTokenId");
                        });

                    b.Navigation("CreatedByIp")
                        .IsRequired();

                    b.Navigation("CreationDate")
                        .IsRequired();

                    b.Navigation("Expiration")
                        .IsRequired();

                    b.Navigation("ReasonRevoked");

                    b.Navigation("ReplacedByToken");

                    b.Navigation("Revoked");

                    b.Navigation("RevokedByIp");

                    b.Navigation("Token")
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WildForest.Domain.Users.Entities.User", b =>
                {
                    b.HasOne("WildForest.Domain.Cities.Entities.City", "City")
                        .WithMany("Users")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("WildForest.Domain.Users.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<string>("UserId")
                                .HasColumnType("text");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("Email");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("WildForest.Domain.Users.ValueObjects.FirstName", "FirstName", b1 =>
                        {
                            b1.Property<string>("UserId")
                                .HasColumnType("text");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("FirstName");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("WildForest.Domain.Users.ValueObjects.LastName", "LastName", b1 =>
                        {
                            b1.Property<string>("UserId")
                                .HasColumnType("text");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("LastName");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.OwnsOne("WildForest.Domain.Users.ValueObjects.Password", "Password", b1 =>
                        {
                            b1.Property<string>("UserId")
                                .HasColumnType("text");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("Password");

                            b1.HasKey("UserId");

                            b1.ToTable("Users");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("City");

                    b.Navigation("Email")
                        .IsRequired();

                    b.Navigation("FirstName")
                        .IsRequired();

                    b.Navigation("LastName")
                        .IsRequired();

                    b.Navigation("Password")
                        .IsRequired();
                });

            modelBuilder.Entity("WildForest.Domain.Weather.Entities.WeatherForecast", b =>
                {
                    b.HasOne("WildForest.Domain.Cities.Entities.City", "City")
                        .WithMany("WeatherForecasts")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("WildForest.Domain.Weather.ValueObjects.Cloudiness", "Cloudiness", b1 =>
                        {
                            b1.Property<string>("WeatherForecastId")
                                .HasColumnType("text");

                            b1.Property<byte>("Value")
                                .HasColumnType("smallint")
                                .HasColumnName("Cloudiness");

                            b1.HasKey("WeatherForecastId");

                            b1.ToTable("WeatherForecasts");

                            b1.WithOwner()
                                .HasForeignKey("WeatherForecastId");
                        });

                    b.OwnsOne("WildForest.Domain.Weather.ValueObjects.ForecastDate", "ForecastDate", b1 =>
                        {
                            b1.Property<string>("WeatherForecastId")
                                .HasColumnType("text");

                            b1.Property<DateOnly>("Value")
                                .HasColumnType("date")
                                .HasColumnName("ForecastDate");

                            b1.HasKey("WeatherForecastId");

                            b1.ToTable("WeatherForecasts");

                            b1.WithOwner()
                                .HasForeignKey("WeatherForecastId");
                        });

                    b.OwnsOne("WildForest.Domain.Weather.ValueObjects.ForecastTime", "ForecastTime", b1 =>
                        {
                            b1.Property<string>("WeatherForecastId")
                                .HasColumnType("text");

                            b1.Property<TimeOnly>("Value")
                                .HasColumnType("time without time zone")
                                .HasColumnName("ForecastTime");

                            b1.HasKey("WeatherForecastId");

                            b1.ToTable("WeatherForecasts");

                            b1.WithOwner()
                                .HasForeignKey("WeatherForecastId");
                        });

                    b.OwnsOne("WildForest.Domain.Weather.ValueObjects.Humidity", "Humidity", b1 =>
                        {
                            b1.Property<string>("WeatherForecastId")
                                .HasColumnType("text");

                            b1.Property<byte>("Value")
                                .HasColumnType("smallint")
                                .HasColumnName("Humidity");

                            b1.HasKey("WeatherForecastId");

                            b1.ToTable("WeatherForecasts");

                            b1.WithOwner()
                                .HasForeignKey("WeatherForecastId");
                        });

                    b.OwnsOne("WildForest.Domain.Weather.ValueObjects.PrecipitationProbability", "PrecipitationProbability", b1 =>
                        {
                            b1.Property<string>("WeatherForecastId")
                                .HasColumnType("text");

                            b1.Property<byte>("Value")
                                .HasColumnType("smallint")
                                .HasColumnName("PrecipitationProbability");

                            b1.HasKey("WeatherForecastId");

                            b1.ToTable("WeatherForecasts");

                            b1.WithOwner()
                                .HasForeignKey("WeatherForecastId");
                        });

                    b.OwnsOne("WildForest.Domain.Weather.ValueObjects.PrecipitationVolume", "PrecipitationVolume", b1 =>
                        {
                            b1.Property<string>("WeatherForecastId")
                                .HasColumnType("text");

                            b1.Property<double?>("Value")
                                .HasColumnType("double precision")
                                .HasColumnName("PrecipitationVolume");

                            b1.HasKey("WeatherForecastId");

                            b1.ToTable("WeatherForecasts");

                            b1.WithOwner()
                                .HasForeignKey("WeatherForecastId");
                        });

                    b.OwnsOne("WildForest.Domain.Weather.ValueObjects.Pressure", "Pressure", b1 =>
                        {
                            b1.Property<string>("WeatherForecastId")
                                .HasColumnType("text");

                            b1.Property<int>("Value")
                                .HasColumnType("integer")
                                .HasColumnName("Pressure");

                            b1.HasKey("WeatherForecastId");

                            b1.ToTable("WeatherForecasts");

                            b1.WithOwner()
                                .HasForeignKey("WeatherForecastId");
                        });

                    b.OwnsOne("WildForest.Domain.Weather.ValueObjects.Temperature", "Temperature", b1 =>
                        {
                            b1.Property<string>("WeatherForecastId")
                                .HasColumnType("text");

                            b1.Property<double>("Value")
                                .HasColumnType("double precision")
                                .HasColumnName("Temperature");

                            b1.Property<double>("ValueFeelsLike")
                                .HasColumnType("double precision")
                                .HasColumnName("TemperatureFeelsLike");

                            b1.HasKey("WeatherForecastId");

                            b1.ToTable("WeatherForecasts");

                            b1.WithOwner()
                                .HasForeignKey("WeatherForecastId");
                        });

                    b.OwnsOne("WildForest.Domain.Weather.ValueObjects.Visibility", "Visibility", b1 =>
                        {
                            b1.Property<string>("WeatherForecastId")
                                .HasColumnType("text");

                            b1.Property<double>("Value")
                                .HasColumnType("double precision")
                                .HasColumnName("Visibility");

                            b1.HasKey("WeatherForecastId");

                            b1.ToTable("WeatherForecasts");

                            b1.WithOwner()
                                .HasForeignKey("WeatherForecastId");
                        });

                    b.OwnsOne("WildForest.Domain.Weather.ValueObjects.WeatherDescription", "WeatherDescription", b1 =>
                        {
                            b1.Property<string>("WeatherForecastId")
                                .HasColumnType("text");

                            b1.Property<string>("Description")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("WeatherDescription");

                            b1.Property<string>("Name")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("WeatherName");

                            b1.HasKey("WeatherForecastId");

                            b1.ToTable("WeatherForecasts");

                            b1.WithOwner()
                                .HasForeignKey("WeatherForecastId");
                        });

                    b.OwnsOne("WildForest.Domain.Weather.ValueObjects.Wind", "Wind", b1 =>
                        {
                            b1.Property<string>("WeatherForecastId")
                                .HasColumnType("text");

                            b1.Property<int>("Direction")
                                .HasColumnType("integer")
                                .HasColumnName("WindDirection");

                            b1.Property<double>("Gust")
                                .HasColumnType("double precision")
                                .HasColumnName("WindGust");

                            b1.Property<double>("Speed")
                                .HasColumnType("double precision")
                                .HasColumnName("WindSpeed");

                            b1.HasKey("WeatherForecastId");

                            b1.ToTable("WeatherForecasts");

                            b1.WithOwner()
                                .HasForeignKey("WeatherForecastId");
                        });

                    b.Navigation("City");

                    b.Navigation("Cloudiness")
                        .IsRequired();

                    b.Navigation("ForecastDate")
                        .IsRequired();

                    b.Navigation("ForecastTime")
                        .IsRequired();

                    b.Navigation("Humidity")
                        .IsRequired();

                    b.Navigation("PrecipitationProbability")
                        .IsRequired();

                    b.Navigation("PrecipitationVolume");

                    b.Navigation("Pressure")
                        .IsRequired();

                    b.Navigation("Temperature")
                        .IsRequired();

                    b.Navigation("Visibility")
                        .IsRequired();

                    b.Navigation("WeatherDescription")
                        .IsRequired();

                    b.Navigation("Wind")
                        .IsRequired();
                });

            modelBuilder.Entity("WildForest.Domain.WeatherMarks.Entities.WeatherMark", b =>
                {
                    b.HasOne("WildForest.Domain.Weather.Entities.WeatherForecast", "WeatherForecast")
                        .WithOne("WeatherMark")
                        .HasForeignKey("WildForest.Domain.WeatherMarks.Entities.WeatherMark", "WeatherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("WildForest.Domain.WeatherMarks.ValueObjects.CountOfMarks", "CountOfMarks", b1 =>
                        {
                            b1.Property<string>("WeatherMarkId")
                                .HasColumnType("text");

                            b1.Property<long>("Value")
                                .HasColumnType("bigint")
                                .HasColumnName("CountOfMarks");

                            b1.HasKey("WeatherMarkId");

                            b1.ToTable("WeatherMarks");

                            b1.WithOwner()
                                .HasForeignKey("WeatherMarkId");
                        });

                    b.OwnsOne("WildForest.Domain.WeatherMarks.ValueObjects.MediumMark", "MediumMark", b1 =>
                        {
                            b1.Property<string>("WeatherMarkId")
                                .HasColumnType("text");

                            b1.Property<double>("Value")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("double precision")
                                .HasDefaultValue(0.0)
                                .HasColumnName("MediumMark");

                            b1.HasKey("WeatherMarkId");

                            b1.ToTable("WeatherMarks");

                            b1.WithOwner()
                                .HasForeignKey("WeatherMarkId");
                        });

                    b.Navigation("CountOfMarks")
                        .IsRequired();

                    b.Navigation("MediumMark")
                        .IsRequired();

                    b.Navigation("WeatherForecast");
                });

            modelBuilder.Entity("WildForest.Domain.Cities.Entities.City", b =>
                {
                    b.Navigation("Users");

                    b.Navigation("WeatherForecasts");
                });

            modelBuilder.Entity("WildForest.Domain.Countries.Entities.Country", b =>
                {
                    b.Navigation("Cities");
                });

            modelBuilder.Entity("WildForest.Domain.Users.Entities.User", b =>
                {
                    b.Navigation("Marks");

                    b.Navigation("RefreshTokens");
                });

            modelBuilder.Entity("WildForest.Domain.Weather.Entities.WeatherForecast", b =>
                {
                    b.Navigation("Marks");

                    b.Navigation("WeatherMark")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
