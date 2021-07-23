package com.ryan.user.service.ryanuserservice.model.response;

public class NewUserResponse extends BaseResponse {
    private String name;
    private String userName;

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

    @Override
    public String toString() {
        return "NewUserResponse{" +
                "success=" + success +
                ", name='" + name + '\'' +
                ", userName='" + userName + '\'' +
                '}';
    }
}
