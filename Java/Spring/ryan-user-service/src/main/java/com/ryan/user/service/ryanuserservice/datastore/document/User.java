package com.ryan.user.service.ryanuserservice.datastore.document;

import org.springframework.data.annotation.Id;
import org.springframework.data.mongodb.core.mapping.Document;

@Document
public class User {

//    @Id
//    private String id;
    private String userId;
    private String name;
    private String userName;
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
