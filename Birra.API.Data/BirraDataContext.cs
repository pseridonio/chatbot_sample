using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Birra.API.Data
{
    public class BirraDataContext : DbContext
    {
        public DbSet<Entities.Question> Questions { get; set; }
        public DbSet<Entities.Answer> Answers { get; set; }
        public DbSet<Entities.Message> Messages { get; set; }
        public DbSet<Entities.RelationQuestionAnswer> RelationQuestionAnswers {get;set;}
        public DbSet<Entities.CurrentSession> CurrentSessions { get; set; }
        public BirraDataContext([NotNullAttribute]DbContextOptions options): base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            this.CreateQuestionTable(modelBuilder);
            this.CreateAnswerTable(modelBuilder);
            this.CreateMessageTable(modelBuilder);
            this.CreateRelationQuestionAnswerTable(modelBuilder);
            this.CreateCurrentSessionTable(modelBuilder);
            base.OnModelCreating(modelBuilder);

        }

        private void CreateQuestionTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Question>(questionEntity =>
            {
                questionEntity.ToTable("TB_QUESTIONS");

                questionEntity
                    .HasKey(question => question.QuestionID)
                    .HasName("PK_QUESTIONS");

                questionEntity
                    .Property(question => question.QuestionID)
                    .HasColumnName("QUESTION_ID")
                    .HasColumnType("INTEGER")
                    .IsRequired();

                questionEntity
                    .Property(question => question.QuestionText)
                    .HasColumnName("QUESTION_TEXT")
                    .HasColumnType("VARCHAR(250)")
                    .IsRequired();
            });
        }
        private void CreateAnswerTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Answer>(answerEntity =>
            {
                answerEntity.ToTable("TB_ANSWERS");

                answerEntity
                    .HasKey(answer => answer.AnswerID)
                    .HasName("PK_ANSWERS");

                answerEntity
                    .Property(answer => answer.AnswerID)
                    .HasColumnName("ANSWER_ID")
                    .HasColumnType("INTEGER")
                    .IsRequired();

                answerEntity
                    .Property(answer => answer.AnswerText)
                    .HasColumnName("ANSWER_TEXT")
                    .HasColumnType("VARCHAR(250)")
                    .IsRequired();
            });
        }
        private void CreateMessageTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Message>(messageEntity =>
            {
                messageEntity.ToTable("TB_MESSAGES");

                messageEntity
                    .HasKey(message => message.MessageID)
                    .HasName("PK_MESSAGES");

                messageEntity
                    .Property(message => message.MessageID)
                    .HasColumnName("MESSAGE_ID")
                    .HasColumnType("INTEGER")
                    .IsRequired();

                messageEntity
                    .Property(message => message.MessageText)
                    .HasColumnName("MESSAGE_TEXT")
                    .HasColumnType("VARCHAR(250)")
                    .IsRequired();
            });
        }
        private void CreateRelationQuestionAnswerTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.RelationQuestionAnswer>(relationQuestionAnswerEntity =>
            {
                relationQuestionAnswerEntity.ToTable("TB_RELATION_QUESTION_ANSWERS");

                relationQuestionAnswerEntity
                    .HasKey(relationQuestionAnswer => new { relationQuestionAnswer.QuestionID, relationQuestionAnswer.AnswerID })
                    .HasName("PK_RELATION_QUESTION_ANSWERS");

                relationQuestionAnswerEntity
                    .Property(relationQuestionAnswer => relationQuestionAnswer.QuestionID)
                    .HasColumnName("QUESTION_ID")
                    .HasColumnType("INTEGER")
                    .IsRequired();

                relationQuestionAnswerEntity
                    .Property(relationQuestionAnswer => relationQuestionAnswer.AnswerID)
                    .HasColumnName("ANSWER_ID")
                    .HasColumnType("INTEGER")
                    .IsRequired();

                relationQuestionAnswerEntity
                    .Property(relationQuestionAnswer => relationQuestionAnswer.NextQuestionID)
                    .HasColumnName("NEXT_QUESTION_ID")
                    .HasColumnType("INTEGER");

                relationQuestionAnswerEntity
                    .Property(relationQuestionAnswer => relationQuestionAnswer.MessageID)
                    .HasColumnName("MESSAGE_ID")
                    .HasColumnType("INTEGER");

                relationQuestionAnswerEntity
                    .HasOne(relationQuestionAnswer => relationQuestionAnswer.Question)
                    .WithMany(question => question.RelationQuestionAnswers)
                    .HasForeignKey(relationQuestionAnswer => relationQuestionAnswer.QuestionID)
                    .HasConstraintName("FK1_RELATION_QUESTION_ANSWERS")
                    .OnDelete(DeleteBehavior.Restrict);

                relationQuestionAnswerEntity
                    .HasOne(relationQuestionAnswer => relationQuestionAnswer.Answer)
                    .WithMany(answer => answer.RelationQuestionAnswers)
                    .HasForeignKey(relationQuestionAnswer => relationQuestionAnswer.AnswerID)
                    .HasConstraintName("FK2_RELATION_QUESTION_ANSWERS")
                    .OnDelete(DeleteBehavior.Restrict);

                relationQuestionAnswerEntity
                    .HasOne(relationQuestionAnswer => relationQuestionAnswer.Message)
                    .WithMany(message => message.RelationQuestionAnswers)
                    .HasForeignKey(relationQuestionAnswer => relationQuestionAnswer.MessageID)
                    .HasConstraintName("FK3_RELATION_QUESTION_ANSWERS")
                    .OnDelete(DeleteBehavior.Restrict);

                relationQuestionAnswerEntity
                    .HasOne(relationQuestionAnswer => relationQuestionAnswer.NextQuestion)
                    .WithMany(question => question.PreviewsRelationQuestionAnswers)
                    .HasForeignKey(relationQuestionAnswer => relationQuestionAnswer.NextQuestionID)
                    .HasConstraintName("FK4_RELATION_QUESTION_ANSWERS")
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
        private void CreateCurrentSessionTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.CurrentSession>(currentSessionEntity =>
            {
                currentSessionEntity.ToTable("TB_CURRENT_SESSIONS");

                currentSessionEntity
                    .HasKey(currentSession => currentSession.CurrentSessionID)
                    .HasName("PK_CURRENT_SESSIONS");

                currentSessionEntity
                    .Property(currentSession => currentSession.CurrentSessionID)
                    .HasColumnName("CURRENT_SESSION_ID")
                    .HasColumnType("VARCHAR(50)")
                    .IsRequired();

                currentSessionEntity
                    .Property(currentSession => currentSession.LastInteraction)
                    .HasColumnName("LAST_INTERACTION")
                    .HasColumnType("DATETIME")
                    .IsRequired();

                currentSessionEntity
                    .Property(currentSession => currentSession.CurrentQuestionID)
                    .HasColumnName("CURRENT_QUESTION_ID")
                    .HasColumnType("INTEGER")
                    .IsRequired();
            });
        }
    }
}
