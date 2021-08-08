package com.ryan.user.service.ryanuserservice.datastore.document;

import org.bson.types.ObjectId;
import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.index.Indexed;
import org.springframework.data.mongodb.core.mapping.Document;
import org.springframework.data.mongodb.core.mapping.Field;

@Document
public class User {

    @Id
    private ObjectId id;
    @Field(name = "user_id")
    @Indexed(unique = true)
    private String userId;
    @Field(name = "username")
    @Indexed(unique = true)
    private String userName;
    @Field(name = "name")
    private String name;
    @Field(name = "created_at")
    private Long createdAt;

    public User(String userId, String name, String userName, Long createdAt) {
        this.userId = userId;
        this.name = name;
        this.userName = userName;
        this.createdAt = createdAt;
    }

    public String getUserId() {
        return userId;
    }

    public void setUserId(String userId) {
        this.userId = userId;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getUserName() {
        return userName;
    }

    public void setUserName(String userName) {
        this.userName = userName;
    }

    public Long getCreatedAt() {
        return createdAt;
    }

    public void setCreatedAt(Long createdAt) {
        this.createdAt = createdAt;
    }
}
