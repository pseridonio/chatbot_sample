﻿// <auto-generated />
using System;
using Birra.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Birra.API.Data.Migrations
{
    [DbContext(typeof(BirraDataContext))]
    [Migration("20210410191406_InitialDatabaseSetup")]
    partial class InitialDatabaseSetup
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("Birra.API.Data.Entities.Answer", b =>
                {
                    b.Property<int>("AnswerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("ANSWER_ID");

                    b.Property<string>("AnswerText")
                        .IsRequired()
                        .HasColumnType("VARCHAR(250)")
                        .HasColumnName("ANSWER_TEXT");

                    b.HasKey("AnswerID")
                        .HasName("PK_ANSWERS");

                    b.ToTable("TB_ANSWERS");
                });

            modelBuilder.Entity("Birra.API.Data.Entities.Message", b =>
                {
                    b.Property<int>("MessageID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("MESSAGE_ID");

                    b.Property<string>("MessageText")
                        .IsRequired()
                        .HasColumnType("VARCHAR(250)")
                        .HasColumnName("MESSAGE_TEXT");

                    b.HasKey("MessageID")
                        .HasName("PK_MESSAGES");

                    b.ToTable("TB_MESSAGES");
                });

            modelBuilder.Entity("Birra.API.Data.Entities.Question", b =>
                {
                    b.Property<int>("QuestionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasColumnName("QUESTION_ID");

                    b.Property<string>("QuestionHint")
                        .HasColumnType("TEXT");

                    b.Property<string>("QuestionText")
                        .IsRequired()
                        .HasColumnType("VARCHAR(250)")
                        .HasColumnName("QUESTION_TEXT");

                    b.HasKey("QuestionID")
                        .HasName("PK_QUESTIONS");

                    b.ToTable("TB_QUESTIONS");
                });

            modelBuilder.Entity("Birra.API.Data.Entities.RelationQuestionAnswer", b =>
                {
                    b.Property<int>("QuestionID")
                        .HasColumnType("INTEGER")
                        .HasColumnName("QUESTION_ID");

                    b.Property<int>("AnswerID")
                        .HasColumnType("INTEGER")
                        .HasColumnName("ANSWER_ID");

                    b.Property<int?>("MessageID")
                        .HasColumnType("INTEGER")
                        .HasColumnName("MESSAGE_ID");

                    b.Property<int?>("NextQuestionID")
                        .HasColumnType("INTEGER")
                        .HasColumnName("NEXT_QUESTION_ID");

                    b.HasKey("QuestionID", "AnswerID")
                        .HasName("PK_RELATION_QUESTION_ANSWERS");

                    b.HasIndex("AnswerID");

                    b.HasIndex("MessageID");

                    b.HasIndex("NextQuestionID");

                    b.ToTable("TB_RELATION_QUESTION_ANSWERS");
                });

            modelBuilder.Entity("Birra.API.Data.Entities.RelationQuestionAnswer", b =>
                {
                    b.HasOne("Birra.API.Data.Entities.Answer", "Answer")
                        .WithMany("RelationQuestionAnswers")
                        .HasForeignKey("AnswerID")
                        .HasConstraintName("FK2_RELATION_QUESTION_ANSWERS")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Birra.API.Data.Entities.Message", "Message")
                        .WithMany("RelationQuestionAnswers")
                        .HasForeignKey("MessageID")
                        .HasConstraintName("FK3_RELATION_QUESTION_ANSWERS")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Birra.API.Data.Entities.Question", "NextQuestion")
                        .WithMany("PreviewsRelationQuestionAnswers")
                        .HasForeignKey("NextQuestionID")
                        .HasConstraintName("FK4_RELATION_QUESTION_ANSWERS")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Birra.API.Data.Entities.Question", "Question")
                        .WithMany("RelationQuestionAnswers")
                        .HasForeignKey("QuestionID")
                        .HasConstraintName("FK1_RELATION_QUESTION_ANSWERS")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Answer");

                    b.Navigation("Message");

                    b.Navigation("NextQuestion");

                    b.Navigation("Question");
                });

            modelBuilder.Entity("Birra.API.Data.Entities.Answer", b =>
                {
                    b.Navigation("RelationQuestionAnswers");
                });

            modelBuilder.Entity("Birra.API.Data.Entities.Message", b =>
                {
                    b.Navigation("RelationQuestionAnswers");
                });

            modelBuilder.Entity("Birra.API.Data.Entities.Question", b =>
                {
                    b.Navigation("PreviewsRelationQuestionAnswers");

                    b.Navigation("RelationQuestionAnswers");
                });
#pragma warning restore 612, 618
        }
    }
}
