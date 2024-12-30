namespace GOV.Messages;

class Message {
    // For future proofing, if needed
}

class EmailMessage : Message {
    public string Title { get; set; } = "";
    public string Text { get; set; } = "";
}