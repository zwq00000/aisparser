﻿namespace AisParser {
    /// <summary>
    ///     AIS Message 12 class
    ///     Addressed Safety Related Message
    /// </summary>
    public class Message12 : Messages {
        /// <summary>
        ///     2 bits   : Sequence
        /// </summary>
        public int Sequence { get; private set; }

        /// <summary>
        ///     30 bits  : Destination MMSI
        /// </summary>
        public long Destination { get; private set; }

        /// <summary>
        ///     1 bit    : Retransmit
        /// </summary>
        public int Retransmit { get; private set; }

        /// <summary>
        ///     1 bit    : Spare
        /// </summary>
        public int Spare { get; private set; }

        /// <summary>
        ///     936 bits : Message in ASCII
        /// </summary>
        public string Message { get; private set; }

        public Message12() : base(12) { }

        /// <summary>
        ///     Subclasses need to override with their own parsing method
        /// </summary>
        /// <param name="sixState"></param>
        /// <exception cref="SixbitsExhaustedException"></exception>
        /// <exception cref="AisMessageException"></exception>
        protected override void Parse(Sixbit sixState) {
            var length = sixState.BitLength();
            if (length < 72 || length > 1008) throw new AisMessageException("Message 12 wrong length");

            base.Parse(sixState);

            Sequence = (int) sixState.Get(2);
            Destination = sixState.Get(30);
            Retransmit = (int) sixState.Get(1);
            Spare = (int) sixState.Get(1);

            Message = sixState.get_string((length - 72) / 6);
        }
    }
}