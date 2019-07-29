// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MovieList.Data.Migrations
{
    [DbContext(typeof(MovieContext))]
    partial class MovieContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0-preview7.19362.6");

            modelBuilder.Entity("MovieList.Data.Models.Kind", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ColorForMovie")
                        .IsRequired();

                    b.Property<string>("ColorForSeries")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.ToTable("Kinds");
                });

            modelBuilder.Entity("MovieList.Data.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImdbLink")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<bool>("IsReleased");

                    b.Property<bool>("IsWatched");

                    b.Property<int>("KindId");

                    b.Property<string>("PosterUrl")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.HasIndex("KindId");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MovieList.Data.Models.MovieSeries", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DisplayNumber")
                        .IsRequired();

                    b.Property<bool>("IsLooselyConnected");

                    b.Property<int?>("OrdinalNumber")
                        .IsRequired();

                    b.Property<int?>("ParentSeriesId")
                        .IsRequired();

                    b.Property<string>("PosterUrl")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("ParentSeriesId");

                    b.ToTable("MovieSeries");
                });

            modelBuilder.Entity("MovieList.Data.Models.MovieSeriesEntry", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("DisplayNumber");

                    b.Property<int?>("MovieId");

                    b.Property<int>("MovieSeriesId");

                    b.Property<int>("OrdinalNumber");

                    b.Property<int?>("SeriesId");

                    b.HasKey("Id");

                    b.HasIndex("MovieId")
                        .IsUnique();

                    b.HasIndex("MovieSeriesId");

                    b.HasIndex("SeriesId")
                        .IsUnique();

                    b.ToTable("MovieSeriesEntries");
                });

            modelBuilder.Entity("MovieList.Data.Models.Period", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EndMonth");

                    b.Property<int>("EndYear");

                    b.Property<bool>("IsSingleDayRelease");

                    b.Property<int>("NumberOfEpisodes");

                    b.Property<string>("PosterUrl")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<int>("SeasonId");

                    b.Property<int>("StartMonth");

                    b.Property<int>("StartYear");

                    b.HasKey("Id");

                    b.HasIndex("SeasonId");

                    b.ToTable("Periods");
                });

            modelBuilder.Entity("MovieList.Data.Models.Season", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Channel")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<bool>("IsReleased");

                    b.Property<bool>("IsWatched");

                    b.Property<int>("OrdinalNumber");

                    b.Property<int>("SeriesId");

                    b.HasKey("Id");

                    b.HasIndex("SeriesId");

                    b.ToTable("Seasons");
                });

            modelBuilder.Entity("MovieList.Data.Models.Series", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ImdbLink")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<bool>("IsMiniseries");

                    b.Property<bool>("IsWatched");

                    b.Property<int>("KindId");

                    b.Property<string>("PosterUrl")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("KindId");

                    b.ToTable("Series");
                });

            modelBuilder.Entity("MovieList.Data.Models.SpecialEpisode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Channel")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<bool>("IsReleased");

                    b.Property<bool>("IsWatched");

                    b.Property<int>("Month");

                    b.Property<int>("OrdinalNumber");

                    b.Property<string>("PosterUrl")
                        .IsRequired()
                        .HasMaxLength(256);

                    b.Property<int>("SeriesId");

                    b.Property<int>("Year");

                    b.HasKey("Id");

                    b.HasIndex("SeriesId");

                    b.ToTable("SpecialEpisodes");
                });

            modelBuilder.Entity("MovieList.Data.Models.Title", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsOriginal");

                    b.Property<int?>("MovieId");

                    b.Property<int?>("MovieSeriesId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.Property<int>("Priority");

                    b.Property<int?>("SeasonId");

                    b.Property<int?>("SeriesId");

                    b.Property<int?>("SpecialEpisodeId");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("MovieSeriesId");

                    b.HasIndex("SeasonId");

                    b.HasIndex("SeriesId");

                    b.HasIndex("SpecialEpisodeId");

                    b.ToTable("Titles");
                });

            modelBuilder.Entity("MovieList.Data.Models.Movie", b =>
                {
                    b.HasOne("MovieList.Data.Models.Kind", "Kind")
                        .WithMany("Movies")
                        .HasForeignKey("KindId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieList.Data.Models.MovieSeries", b =>
                {
                    b.HasOne("MovieList.Data.Models.MovieSeries", "ParentSeries")
                        .WithMany("Parts")
                        .HasForeignKey("ParentSeriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieList.Data.Models.MovieSeriesEntry", b =>
                {
                    b.HasOne("MovieList.Data.Models.Movie", "Movie")
                        .WithOne("Entry")
                        .HasForeignKey("MovieList.Data.Models.MovieSeriesEntry", "MovieId");

                    b.HasOne("MovieList.Data.Models.MovieSeries", "MovieSeries")
                        .WithMany("Entries")
                        .HasForeignKey("MovieSeriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MovieList.Data.Models.Series", "Series")
                        .WithOne("Entry")
                        .HasForeignKey("MovieList.Data.Models.MovieSeriesEntry", "SeriesId");
                });

            modelBuilder.Entity("MovieList.Data.Models.Period", b =>
                {
                    b.HasOne("MovieList.Data.Models.Season", "Season")
                        .WithMany("Periods")
                        .HasForeignKey("SeasonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieList.Data.Models.Season", b =>
                {
                    b.HasOne("MovieList.Data.Models.Series", "Series")
                        .WithMany("Seasons")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieList.Data.Models.Series", b =>
                {
                    b.HasOne("MovieList.Data.Models.Kind", "Kind")
                        .WithMany("Series")
                        .HasForeignKey("KindId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieList.Data.Models.SpecialEpisode", b =>
                {
                    b.HasOne("MovieList.Data.Models.Series", "Series")
                        .WithMany("SpecialEpisodes")
                        .HasForeignKey("SeriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieList.Data.Models.Title", b =>
                {
                    b.HasOne("MovieList.Data.Models.Movie", "Movie")
                        .WithMany("Titles")
                        .HasForeignKey("MovieId");

                    b.HasOne("MovieList.Data.Models.MovieSeries", "MovieSeries")
                        .WithMany("Titles")
                        .HasForeignKey("MovieSeriesId");

                    b.HasOne("MovieList.Data.Models.Season", "Season")
                        .WithMany("Titles")
                        .HasForeignKey("SeasonId");

                    b.HasOne("MovieList.Data.Models.Series", "Series")
                        .WithMany("Titles")
                        .HasForeignKey("SeriesId");

                    b.HasOne("MovieList.Data.Models.SpecialEpisode", "SpecialEpisode")
                        .WithMany("Titles")
                        .HasForeignKey("SpecialEpisodeId");
                });
#pragma warning restore 612, 618
        }
    }
}
