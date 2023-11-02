﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WildForest.Infrastructure.Persistence.Context;

#nullable disable

namespace WildForest.Infrastructure.Migrations
{
    [DbContext(typeof(WildForestDbContext))]
    partial class WildForestDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("WildForest.Domain.Cities.Entities.City", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CountryId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("WildForest.Domain.Countries.Entities.Country", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("WildForest.Domain.Languages.Entities.Language", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("WildForest.Domain.Ratings.Entities.Vote", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("RatingId")
                        .HasColumnType("uuid");

                    b.Property<int>("Result")
                        .HasColumnType("integer")
                        .HasColumnName("Result");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RatingId");

                    b.HasIndex("UserId");

                    b.ToTable("Votes");
                });

            modelBuilder.Entity("WildForest.Domain.Ratings.Rating", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<int>("Points")
                        .HasColumnType("integer")
                        .HasColumnName("Points");

                    b.Property<Guid>("WeatherForecastId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("WeatherForecastId")
                        .IsUnique();

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("WildForest.Domain.Tokens.Entities.RefreshToken", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("CreatedByIp")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("CreatedByIp");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("CreationDate");

                    b.Property<DateTime>("Expiration")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("Expiration");

                    b.Property<string>("ReasonRevoked")
                        .HasColumnType("text")
                        .HasColumnName("ReasonRevoked");

                    b.Property<string>("ReplacedByToken")
                        .HasColumnType("text")
                        .HasColumnName("ReplacedByToken");

                    b.Property<string>("RevokedByIp")
                        .HasColumnType("text")
                        .HasColumnName("RevokedByIp");

                    b.Property<DateTime?>("RevokedDate")
                        .HasColumnType("timestamp without time zone")
                        .HasColumnName("RevokedDate");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Token");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("WildForest.Domain.Users.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CityId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("LanguageId")
                        .HasColumnType("uuid");

                    b.Property<int>("Role")
                        .HasColumnType("integer")
                        .HasColumnName("Role");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("LanguageId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WildForest.Domain.Weather.WeatherForecast", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CityId")
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date")
                        .HasColumnName("Date");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("WeatherForecasts", (string)null);
                });

            modelBuilder.Entity("WildForest.Domain.Cities.Entities.City", b =>
                {
                    b.HasOne("WildForest.Domain.Countries.Entities.Country", "Country")
                        .WithMany("Cities")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("WildForest.Domain.Cities.ValueObjects.CityName", "Name", b1 =>
                        {
                            b1.Property<Guid>("CityId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Name");

                            b1.HasKey("CityId");

                            b1.ToTable("Cities");

                            b1.WithOwner()
                                .HasForeignKey("CityId");
                        });

                    b.OwnsOne("WildForest.Domain.Cities.ValueObjects.Location", "Location", b1 =>
                        {
                            b1.Property<Guid>("CityId")
                                .HasColumnType("uuid");

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

                    b.Navigation("Country");

                    b.Navigation("Location")
                        .IsRequired();

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("WildForest.Domain.Countries.Entities.Country", b =>
                {
                    b.OwnsOne("WildForest.Domain.Countries.ValueObjects.CountryName", "Name", b1 =>
                        {
                            b1.Property<Guid>("CountryId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("character varying(50)")
                                .HasColumnName("Name");

                            b1.HasKey("CountryId");

                            b1.ToTable("Countries");

                            b1.WithOwner()
                                .HasForeignKey("CountryId");
                        });

                    b.Navigation("Name")
                        .IsRequired();
                });

            modelBuilder.Entity("WildForest.Domain.Ratings.Entities.Vote", b =>
                {
                    b.HasOne("WildForest.Domain.Ratings.Rating", "Rating")
                        .WithMany("Votes")
                        .HasForeignKey("RatingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WildForest.Domain.Users.Entities.User", "User")
                        .WithMany("Votes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rating");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WildForest.Domain.Ratings.Rating", b =>
                {
                    b.HasOne("WildForest.Domain.Weather.WeatherForecast", "WeatherForecast")
                        .WithOne("Rating")
                        .HasForeignKey("WildForest.Domain.Ratings.Rating", "WeatherForecastId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("WeatherForecast");
                });

            modelBuilder.Entity("WildForest.Domain.Tokens.Entities.RefreshToken", b =>
                {
                    b.HasOne("WildForest.Domain.Users.Entities.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
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

                    b.HasOne("WildForest.Domain.Languages.Entities.Language", "Language")
                        .WithMany("Users")
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("WildForest.Domain.Users.ValueObjects.Email", "Email", b1 =>
                        {
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

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
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

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
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

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
                            b1.Property<Guid>("UserId")
                                .HasColumnType("uuid");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("Password");

                            b1.Property<byte[]>("_salt")
                                .IsRequired()
                                .HasColumnType("bytea")
                                .HasColumnName("Salt");

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

                    b.Navigation("Language");

                    b.Navigation("LastName")
                        .IsRequired();

                    b.Navigation("Password")
                        .IsRequired();
                });

            modelBuilder.Entity("WildForest.Domain.Weather.WeatherForecast", b =>
                {
                    b.HasOne("WildForest.Domain.Cities.Entities.City", "City")
                        .WithMany("WeatherForecasts")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("WildForest.Domain.Weather.Entities.ThreeHourWeatherForecast", "ThreeHourWeatherForecasts", b1 =>
                        {
                            b1.Property<Guid>("Id")
                                .HasColumnType("uuid");

                            b1.Property<TimeOnly>("Time")
                                .HasColumnType("time without time zone")
                                .HasColumnName("Time");

                            b1.Property<Guid>("WeatherForecastId")
                                .HasColumnType("uuid");

                            b1.HasKey("Id");

                            b1.HasIndex("WeatherForecastId");

                            b1.ToTable("ThreeHourWeatherForecasts", (string)null);

                            b1.WithOwner("WeatherForecast")
                                .HasForeignKey("WeatherForecastId");

                            b1.OwnsOne("WildForest.Domain.Weather.ValueObjects.Cloudiness", "Cloudiness", b2 =>
                                {
                                    b2.Property<Guid>("ThreeHourWeatherForecastId")
                                        .HasColumnType("uuid");

                                    b2.Property<byte>("Value")
                                        .HasColumnType("smallint")
                                        .HasColumnName("Cloudiness");

                                    b2.HasKey("ThreeHourWeatherForecastId");

                                    b2.ToTable("ThreeHourWeatherForecasts");

                                    b2.WithOwner()
                                        .HasForeignKey("ThreeHourWeatherForecastId");
                                });

                            b1.OwnsOne("WildForest.Domain.Weather.ValueObjects.Humidity", "Humidity", b2 =>
                                {
                                    b2.Property<Guid>("ThreeHourWeatherForecastId")
                                        .HasColumnType("uuid");

                                    b2.Property<byte>("Value")
                                        .HasColumnType("smallint")
                                        .HasColumnName("Humidity");

                                    b2.HasKey("ThreeHourWeatherForecastId");

                                    b2.ToTable("ThreeHourWeatherForecasts");

                                    b2.WithOwner()
                                        .HasForeignKey("ThreeHourWeatherForecastId");
                                });

                            b1.OwnsOne("WildForest.Domain.Weather.ValueObjects.PrecipitationProbability", "PrecipitationProbability", b2 =>
                                {
                                    b2.Property<Guid>("ThreeHourWeatherForecastId")
                                        .HasColumnType("uuid");

                                    b2.Property<byte>("Value")
                                        .HasColumnType("smallint")
                                        .HasColumnName("PrecipitationProbability");

                                    b2.HasKey("ThreeHourWeatherForecastId");

                                    b2.ToTable("ThreeHourWeatherForecasts");

                                    b2.WithOwner()
                                        .HasForeignKey("ThreeHourWeatherForecastId");
                                });

                            b1.OwnsOne("WildForest.Domain.Weather.ValueObjects.PrecipitationVolume", "PrecipitationVolume", b2 =>
                                {
                                    b2.Property<Guid>("ThreeHourWeatherForecastId")
                                        .HasColumnType("uuid");

                                    b2.Property<double>("Value")
                                        .HasColumnType("double precision")
                                        .HasColumnName("PrecipitationVolume");

                                    b2.HasKey("ThreeHourWeatherForecastId");

                                    b2.ToTable("ThreeHourWeatherForecasts");

                                    b2.WithOwner()
                                        .HasForeignKey("ThreeHourWeatherForecastId");
                                });

                            b1.OwnsOne("WildForest.Domain.Weather.ValueObjects.Pressure", "Pressure", b2 =>
                                {
                                    b2.Property<Guid>("ThreeHourWeatherForecastId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Value")
                                        .HasColumnType("integer")
                                        .HasColumnName("Pressure");

                                    b2.HasKey("ThreeHourWeatherForecastId");

                                    b2.ToTable("ThreeHourWeatherForecasts");

                                    b2.WithOwner()
                                        .HasForeignKey("ThreeHourWeatherForecastId");
                                });

                            b1.OwnsOne("WildForest.Domain.Weather.ValueObjects.Temperature", "Temperature", b2 =>
                                {
                                    b2.Property<Guid>("ThreeHourWeatherForecastId")
                                        .HasColumnType("uuid");

                                    b2.Property<double>("Value")
                                        .HasColumnType("double precision")
                                        .HasColumnName("Temperature");

                                    b2.Property<double>("ValueFeelsLike")
                                        .HasColumnType("double precision")
                                        .HasColumnName("TemperatureFeelsLike");

                                    b2.HasKey("ThreeHourWeatherForecastId");

                                    b2.ToTable("ThreeHourWeatherForecasts");

                                    b2.WithOwner()
                                        .HasForeignKey("ThreeHourWeatherForecastId");
                                });

                            b1.OwnsOne("WildForest.Domain.Weather.ValueObjects.Visibility", "Visibility", b2 =>
                                {
                                    b2.Property<Guid>("ThreeHourWeatherForecastId")
                                        .HasColumnType("uuid");

                                    b2.Property<double>("Value")
                                        .HasColumnType("double precision")
                                        .HasColumnName("Visibility");

                                    b2.HasKey("ThreeHourWeatherForecastId");

                                    b2.ToTable("ThreeHourWeatherForecasts");

                                    b2.WithOwner()
                                        .HasForeignKey("ThreeHourWeatherForecastId");
                                });

                            b1.OwnsOne("WildForest.Domain.Weather.ValueObjects.WeatherDescription", "Description", b2 =>
                                {
                                    b2.Property<Guid>("ThreeHourWeatherForecastId")
                                        .HasColumnType("uuid");

                                    b2.Property<string>("Description")
                                        .IsRequired()
                                        .HasMaxLength(50)
                                        .HasColumnType("character varying(50)")
                                        .HasColumnName("Description");

                                    b2.Property<string>("Name")
                                        .IsRequired()
                                        .HasMaxLength(50)
                                        .HasColumnType("character varying(50)")
                                        .HasColumnName("Name");

                                    b2.HasKey("ThreeHourWeatherForecastId");

                                    b2.ToTable("ThreeHourWeatherForecasts");

                                    b2.WithOwner()
                                        .HasForeignKey("ThreeHourWeatherForecastId");
                                });

                            b1.OwnsOne("WildForest.Domain.Weather.ValueObjects.Wind", "Wind", b2 =>
                                {
                                    b2.Property<Guid>("ThreeHourWeatherForecastId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("Direction")
                                        .HasColumnType("integer")
                                        .HasColumnName("WindDirection");

                                    b2.Property<double>("Gust")
                                        .HasColumnType("double precision")
                                        .HasColumnName("WindGust");

                                    b2.Property<double>("Speed")
                                        .HasColumnType("double precision")
                                        .HasColumnName("WindSpeed");

                                    b2.HasKey("ThreeHourWeatherForecastId");

                                    b2.ToTable("ThreeHourWeatherForecasts");

                                    b2.WithOwner()
                                        .HasForeignKey("ThreeHourWeatherForecastId");
                                });

                            b1.Navigation("Cloudiness")
                                .IsRequired();

                            b1.Navigation("Description")
                                .IsRequired();

                            b1.Navigation("Humidity")
                                .IsRequired();

                            b1.Navigation("PrecipitationProbability")
                                .IsRequired();

                            b1.Navigation("PrecipitationVolume");

                            b1.Navigation("Pressure")
                                .IsRequired();

                            b1.Navigation("Temperature")
                                .IsRequired();

                            b1.Navigation("Visibility")
                                .IsRequired();

                            b1.Navigation("WeatherForecast");

                            b1.Navigation("Wind")
                                .IsRequired();
                        });

                    b.Navigation("City");

                    b.Navigation("ThreeHourWeatherForecasts");
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

            modelBuilder.Entity("WildForest.Domain.Languages.Entities.Language", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("WildForest.Domain.Ratings.Rating", b =>
                {
                    b.Navigation("Votes");
                });

            modelBuilder.Entity("WildForest.Domain.Users.Entities.User", b =>
                {
                    b.Navigation("RefreshTokens");

                    b.Navigation("Votes");
                });

            modelBuilder.Entity("WildForest.Domain.Weather.WeatherForecast", b =>
                {
                    b.Navigation("Rating")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
