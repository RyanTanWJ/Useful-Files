# Kafka
This page serves to document my attempt to explore the usage of Kafka as a message broker for Unity applications.

## Chapters
1. What is Kafka?
2. What is Confluent?
3. Running Kafka
4. Containerised Kafka
5. Kafka with Unity3D
6. Re-evaluating my Use Case
7. Comparing Message Brokers
8. Final Evaluation

# What is Kafka?
Kafka is a message broker built with Java, and typically only supported Java and Scala. Confluent was created as a solution to support more languages.

# What is Confluent?
Confluent is Kafka++. As my use case is to get Kafka to work with Unity, the most important thing to note is that Confluent has a .NET Client that acts as a wrapper for librdkafka, a C client.

# Running Kafka
Kafka required installing Apache Zookeeper and Apache Kafka. It also depended on Java, so I installed all of those. However, even after doing so I was unable to run Kafka. My Command Line and PowerShell windows would close when I tried running Kafka, following the instructions on the Quickstart page. So if Kafka would not work on my environment, perhaps I could run it in its intended environment.

# Containerised Kafka
I saw that Confluent had a containerised version of Kafka, and I followed the guide provided. It worked without issues. I was able to create a topic, populate the stream with messages of that topic, and consume the messages on that topic. With the set up of the broker out of the way, I moved on to setting up a Unity client that could consume the messages.

# Kafka with Unity3D
Confluent already had a .NET client available. Referecing the code provided, I tried to write my own client script on Unity3D. However, I soon hit a hurdle as the code had multiple dependencies on nuget distributions. To continue, I recursively downloaded all the dependencies required, then imported them into Unity. Reviewing my code in the editor, I could now import the dependencies and thus cleared my code of any compilation errors. However, returning to Unity, I received an error stating that the project could not be built. I then went to read the specifications for the dependencies, and found that Unity3D only supported an older version of .NET than what the Confluent .NET client required. I concluded that the limitation was the lack of support and moved on.

# Re-evaluating my Use Case


# Comparing Message Brokers

# Final Evaluation
