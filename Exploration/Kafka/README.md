# Kafka
This page serves to document my attempt to explore the usage of Apache Kafka as a message broker for Unity applications.

## Chapters
1. What is Apache Kafka?
2. What is Confluent?
3. Running Apache Kafka
4. Containerised Apache Kafka
5. Apache Kafka with Unity3D
6. Re-evaluating my Use Case
7. Comparing Message Brokers
8. Final Evaluation

# What is Apache Kafka?
Apache Kafka is a message broker built with Java, and typically only supported Java and Scala. Confluent was created as a solution to support more languages.

# What is Confluent?
Confluent is Kafka++. As my use case is to get Kafka to work with Unity, the most important thing to note is that Confluent has a .NET Client that acts as a wrapper for librdkafka, a C client.

# Running Apache Kafka
Apache Kafka required installing Apache Zookeeper and Apache Kafka. It also depended on Java, so I installed all of those. However, even after doing so I was unable to run Kafka. My Command Line and PowerShell windows would close when I tried running Kafka, following the instructions on the Quickstart page. So if Apache Kafka would not work on my environment, perhaps I could run it in its intended environment.

# Containerised Apache Kafka
I saw that Confluent had a containerised version of Apache Kafka, and I followed the guide provided. It worked without issues. I was able to create a topic, populate the stream with messages of that topic, and consume the messages on that topic. With the set up of the broker out of the way, I moved on to setting up a Unity client that could consume the messages.

# Apache Kafka with Unity3D
Confluent already had a .NET client available. Referecing the code provided, I tried to write my own client script on Unity3D. However, I soon hit a hurdle as the code had multiple dependencies on nuget distributions. To continue, I recursively downloaded all the dependencies required, then imported them into Unity. Reviewing my code in the editor, I could now import the dependencies and thus cleared my code of any compilation errors. However, returning to Unity, I received an error stating that the project could not be built. I then went to read the specifications for the dependencies, and found that Unity3D only supported an older version of .NET than what the Confluent .NET client required. I concluded that the limitation was the lack of support and moved on.

# Re-evaluating my Use Case
Given the problems that arose while trying to install and run Kafka, I had to re-evaluate whether I needed to use Kafka in the first place. Currently, I was already using ActiveMQ for my message broker needs. So I went to compare the features of different message brokers.

# Comparing Message Brokers
I reviewed the features of ActiveMQ, RabbitMQ and Apache Kafka. Firstly as ActiveMQ and RabbitMQ are fairly similar, I will first compare them with Apache Kafka. Then I will compare the differences between ActiveMQ and RabbitMQ.

## ActiveMQ, RabbitMQ vs Kafka
| Comparator         | ActiveMQ/RabbitMQ                               | Kafka                                                         |
|--------------------|-------------------------------------------------|---------------------------------------------------------------|
| Payload Size       | Large JSON/XML                                  | Small Key-Value Pairs, values are usually small atomic values |
| Data Flow          | Distinct bounded messages                       | Unbounded continuous flow                                     |
| Throughput         | 4K(Persistent) - 10K(Non-Persistent) Messages/s | 100K-1M Messages/s                                            |
| Uses               | Transactional Data                              | Operational Data                                              |
| Supported Topology | Point-to-Point<br>Pub/Sub<br>Exchange           | Pub/Sub                                                       |

## ActiveMQ vs RabbitMQ


# Final Evaluation
