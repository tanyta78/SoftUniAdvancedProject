namespace BashSoft.IO.Commands
{
    using System;
    using BashSoft.Contracts;
    using Exceptions;

    public abstract class Command : IExecutable
    {
        private string input;
        private string[] data;

        protected Command(string input, string[] data, IContentComparer judge, IDatabase repository, IDirectoryManager inputOutputManager)
        {
            this.Input = input;
            this.Data = data;
            this.Judge = judge;
            this.Repository = repository;
            this.InputOutputManager = inputOutputManager;
        }

        public string[] Data
        {
            get => this.data;
            private set
            {
                if (value == null || value.Length == 0)
                {
                    throw new NullReferenceException();
                }

                this.data = value;
            }
        }

        public string Input
        {
            get => this.input;

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new InvalidStringException();
                }

                this.input = value;
            }
        }

        protected IContentComparer Judge { get; }

        protected IDatabase Repository { get; }

        protected IDirectoryManager InputOutputManager { get; }

        public abstract void Execute();
    }
}